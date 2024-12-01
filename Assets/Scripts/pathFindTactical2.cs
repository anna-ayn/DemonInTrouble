using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathFindTactical2
{
    [System.NonSerialized]
    public Kinematic character;

    [System.NonSerialized]
    public string typecharacter;

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
        // Iterate through processing each node.
        while (open.list.Count > 0)
        {
            // Find the smallest element in the open list (using 
            // the estimatedTotalCost)
            current = open.smallestElement();
        

            // If it is the goal node, then terminate.
            if (current.node.getId() == goal.getId())
                break;

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

                // to avoid the scrolls
                if (endNode.getHasScroll()) {
                    endNodeCost += 1000.0f;
                }

                NodeRecord endNodeRecord = null;
                
                // If the node is closed we may have to skip, or remove it
                // from the closed list.
                if (closed.contains(endNode)) {
                    // Here we find the record in the closed list.
                    // corresponding to the endNode.
                    endNodeRecord = closed.find(endNode);

                    // If we didnt find a shorter route, skip.
                    if (endNodeRecord.costSoFar <= endNodeCost)
                        continue;
                    
                    // Otherwise, remove it from the closed list.
                    closed.remove(endNodeRecord);

                    // We can use the node's old cost values to calculate
                    // its heuristic without calling the possibily expensive 
                    // heuristic function.
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                }
                
                // Skip if the node is open and we've not found a better route
                else if (open.contains(endNode)) {
                    // Here we find the record in the open list
                    // corresponding to the endNode
                    endNodeRecord = open.find(endNode);

                    // If our route is no better, then skip
                    if (endNodeRecord.costSoFar <= endNodeCost) continue;

                    // Again, we can calculate its heuristic
                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
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

        // We're here if we've either found the goal, or if we've no more
        // nodes to search, find which.
        if (current.node != goal) {
            // We've run out of nodes without finding the coal, so there's
            // no solution.
            return null;
        }
        else
        {
            // Compile the list of connections in the path.
            List<Node> path = new List<Node>();

            // Work back along the path, accumulating connections.
            int id = 0;
            while (current.node.getId() != start.getId())
            {
            
                path.Insert(id, current.node);
                current = current.connection;
                id++;

            }

            // Reverse the path, and return it.
            return path;
        }
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