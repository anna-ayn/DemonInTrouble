using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    private Node fromNode;
    private Node toNode;
    private float cost; 

    public Connection(Node u, Node v, float w)
    {
        this.fromNode = u;
        this.toNode = v;
        this.cost = w;
    }

    public float getCost()
    {
        return cost;
    }

    public Node getFromNode()
    {
        return fromNode;
    }

    public Node getToNode()
    {
        return toNode;
    }
}