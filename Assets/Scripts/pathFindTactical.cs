using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PathFindTactical
{
    [System.NonSerialized]
    public Kinematic character;

    [System.NonSerialized]
    public Graph graph;

    [System.NonSerialized]
    public Node start;

    [System.NonSerialized]
    public Node goal;

    [System.NonSerialized]
    public float maxAcceleration;

    // pathFindAStar function
    public List<Node> pathFindAStar(Heuristic heuristic) {
        Debug.Log("Iniciando pathFindAStar... con startNode: " + start.getCube().name + " con goalNode: " + goal.getCube().name);
        
        // Initialize the record for the start node.
        NodeRecord startRecord = new NodeRecord();
        startRecord.node = start;
        startRecord.connection = null;
        startRecord.costSoFar = 0;
        startRecord.estimatedTotalCost = heuristic.estimate(start);

        // Initialize the open and closed lists.
        PathFindingList open = new PathFindingList();
        open.add(startRecord);
        PathFindingList closed = new PathFindingList();

        NodeRecord current = new NodeRecord();

        List<List<Node>> paths = new List<List<Node>>(); // list of paths
        List<float> costpaths = new List<float>();

        // Iterate through processing each node.
        while (open.list.Count > 0)
        {
            // Find the smallest element in the open list (using 
            // the estimatedTotalCost)
            current = open.smallestElement();

            if (current.node.getId() == goal.getId()) {
                open.remove(current);
                // Compile the list of connections in the path.
                List<Node> path = new List<Node>();

                costpaths.Add(current.costSoFar);

                // Work back along the path, accumulating connections.
                int id = 0;
                while (current.node.getId() != start.getId())
                {
                
                    path.Insert(id, current.node);
                    current = current.connection;
                    id++;

                }

                paths.Add(path);
                continue;
            }

            // Otherwise get its outgoing connections.
            List<Connection> connections = graph.getConnections(current.node);

            float endNodeHeuristic = 0;

            // Loop through each connection in turn.
            foreach(Connection connection in connections)
            {
                // Get the cost estimate for the end node.
                Node endNode = connection.getToNode();
                if (endNode == current.node) {
                    endNode = connection.getFromNode();
                }
                float endNodeCost = current.costSoFar + connection.getCost();

                NodeRecord endNodeRecord = null;

                Debug.Log("From node " + current.node.getCube().name + " to node " + endNode.getCube().name + " with cost " + endNodeCost);

                // If the node is closed we may have to skip, or remove it
                // from the closed list.
                if (closed.contains(endNode)) {
                    Debug.Log("Node " + endNode.getCube().name + " is in closed list");
                    // Here we find the record in the closed list.
                    // corresponding to the endNode.
                    endNodeRecord = closed.find(endNode);

                    // If we didnt find a shorter route, skip.
                    if (endNodeRecord.costSoFar <= endNodeCost) continue;

                    Debug.Log("Nodo NO ignorado");
                    
                    // Otherwise, remove it from the closed list.
                    closed.remove(endNodeRecord);

                    // We can use the node's old cost values to calculate
                    // its heuristic without calling the possibily expensive 
                    // heuristic function.
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                }
                
                // Skip if the node is open and we've not found a better route
                else if (open.contains(endNode)) {
                    Debug.Log("Node " + endNode.getCube().name + " is in open list");
                    // Here we find the record in the open list
                    // corresponding to the endNode
                    endNodeRecord = open.find(endNode);

                    // If our route is no better, then skip
                    if (endNodeRecord.costSoFar <= endNodeCost && current.advantagePoints <= endNodeRecord.advantagePoints) continue;
                    
                    Debug.Log("Nodo NO ignorado " + current.advantagePoints + " " + endNodeRecord.advantagePoints);
                    // Again, we can calculate its heuristic
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;

                    endNodeRecord.advantagePoints = current.advantagePoints;
                }

                // Otherwise we know we've got an unvisited node, so make a
                // record for it.
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;

                    // We'll need to calculate the heuristic value using
                    // the function, since we don't have and existing record to use
                    endNodeHeuristic = heuristic.estimate(endNode);

                    endNodeRecord.advantagePoints = current.advantagePoints;

                    if (endNode.getHasScroll()) {
                        endNodeRecord.advantagePoints++;
                    }
                }
               
                // We're here if we need to update the node. Update the
                // cost, estimate and connection.
                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.connection = current;
                endNodeRecord.estimatedTotalCost = endNodeCost + endNodeHeuristic;

                // And add it to the open list.
                if (!open.contains(endNodeRecord.node))
                    open.add(endNodeRecord);
            }

            // We've finished looking at the connections for the current
            // node, so add it to the closed list and remove it from the open list
            open.remove(current);
            closed.add(current);
        }

        if (paths.Count == 0) {
            return null;
        }
        List<Node> optimal_path = paths[0];
        float cost_optimal = costpaths[0];

        List<Node> tactical_path = optimal_path;
        float cost_tactical = cost_optimal;

        for (int i = 0 ; i < paths.Count; i++) {

            // print route
            string pathString = "";
            for (int j = 0; j < paths[i].Count; j++) {
                Node n = paths[i][j];
                pathString += n.getCube().name + " ";
            }
            Debug.Log(pathString + " cost: " + costpaths[i]);

            List<Node> route = paths[i];
            float costTotal = costpaths[i];

            // count how many nodes have scrolls
            int count = 0;
            foreach (Node n in route) {
                if (n.getHasScroll()) {
                    count++;
                }
            }

            if (count > 0) {
                float temp = costTotal / (2 + count);
                if (temp <= cost_optimal && temp <= cost_tactical) {
                    tactical_path = route;
                    cost_tactical = temp;
                }
            }
        }

        // return a pair of optimal path and tactical path
        return tactical_path;
    }

    public virtual SteeringOutput getSteering(Node target)
    {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target.
        result.linear = target.getCube().transform.position - character.position;

        // Give full acceleration along this direction
        if (result.linear.magnitude > maxAcceleration) {
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }
        
        result.angular = 0;
        return result;
    }
    
}