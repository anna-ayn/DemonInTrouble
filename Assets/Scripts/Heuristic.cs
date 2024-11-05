using System.Collections.Generic;
using UnityEngine;

// Heuristic
public class Heuristic
{
    // Stores the goal node that this heuristic is estimating for.
    public Node goalNode;

    public Heuristic(Node goalNode) {
        this.goalNode = goalNode;
    }

    // Estimated cost to move betwen any two nodes.
    public float estimate(Node fromNode, Node toNode) {
        return Vector3.Distance(fromNode.getCube().transform.position, toNode.getCube().transform.position); 
    }

    // Estimated cost to reach the stored goal from the given node.
    public float estimate(Node fromNode) {
        return estimate(fromNode, goalNode);
    }

}