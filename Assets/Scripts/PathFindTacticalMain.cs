using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFindTacticalMain : MonoBehaviour
{
    public Agent character;
    public Graph graph;

    public PathFindTactical algorithm;

    public List<Node> end_nodes = new List<Node>();

    public List<Node> path = new List<Node>();

    public Node currentNode;

    public float maxAcceleration;

    private void Awake()
    {
        character.Initialize();
        graph = new Graph();
        graph.CreateNodes();
        graph.ConnectNodes();
        graph.setNodesScroll();
        Debug.Log("Grafo creado");

        algorithm = new PathFindTactical();
        algorithm.maxAcceleration = maxAcceleration;

        algorithm.graph = graph;

        // encontrar el nodo de cada gema (cada gema tiene Tag "Gem")
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");

        for (int i = 0; i < gems.Length; i++)
        {
            Node end = graph.FindCube(gems[i].transform.position);
            end_nodes.Add(end);
        }

        algorithm.goal = end_nodes[0];
    }

    private void Update() {
        algorithm.character = character.kinematic;
        algorithm.start = graph.FindCube(character.kinematic.position);

        if (path.Count == 0) {
            path = algorithm.pathFindAStar(new Heuristic(algorithm.goal));
            if (path.Count == 0) return;
            currentNode = path[path.Count - 1];
        }

        if (Vector3.Distance(character.kinematic.position, currentNode.getCube().transform.position) < 5.0f) {
            path.RemoveAt(path.Count - 1);
            if (path.Count > 0) {
                currentNode = path[path.Count - 1];
            }
        }

        if (path.Count == 0) return;
       
        // Clear previous lines
        GameObject[] lines = GameObject.FindGameObjectsWithTag("LinePlayer");
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

        // Draw new lines
        for (int j = 0; j < path.Count - 1; j++)
        {
            GameObject lineObject = new GameObject("LinePlayer");
            lineObject.tag = "LinePlayer";
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.3f;
            lineRenderer.endWidth = 0.3f;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, path[j].getCube().transform.position);
            lineRenderer.SetPosition(1, path[j + 1].getCube().transform.position);
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }

        // move character to the first node
        character.steering = algorithm.getSteering(path[path.Count - 1]);
        character.doUpdate(maxAcceleration);
    }
}
