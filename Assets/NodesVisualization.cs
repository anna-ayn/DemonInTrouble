using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesVisualization : MonoBehaviour
{
    Graph graph;
    // Start is called before the first frame update
    void Start()
    {
        graph = new Graph();
        graph.CreateNodes();
        graph.DrawNodes();
    }
}
