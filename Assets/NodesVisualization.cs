using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesVisualization : MonoBehaviour
{
    public Graph graph;

    public void Initialize()
    {
        graph = new Graph();
        graph.CreateNodes();
        graph.ConnectNodes();
        graph.DrawNodes();
        graph.setNodesScroll();
    }
}
