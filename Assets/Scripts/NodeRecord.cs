
using System.Collections.Generic;
using UnityEngine;


// This structure is used to keep track of the
// information we need for each node.
public class NodeRecord
{
    public Node node;
    public NodeRecord connection;
    public float costSoFar;
    public float estimatedTotalCost;
    public int advantagePoints;

    public NodeRecord() {
        this.advantagePoints = 0;
    }
}