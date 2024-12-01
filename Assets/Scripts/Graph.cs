using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public List<Node> Nodes; // Lista de nodos en el grafo

    public Graph() {
        Nodes = new List<Node>();
    }

    public void CreateNodes()
    {
        int id = 0;
        // Encuentra todos los objetos Cube en la escena
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Squares"); 

        foreach (GameObject cube in cubes)
        {
            Nodes.Add(new Node(id, cube));
            id++;
        }

        Debug.Log("Nodos creados: " + Nodes.Count);
    }

    public void ConnectNodes()
    {
        // conectar cada cubo con su vecino
        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = 0; j < Nodes.Count; j++)
            {
                if (i != j) {
                    float distance = Vector3.Distance(Nodes[i].getCube().transform.position, Nodes[j].getCube().transform.position);
                    if (distance < 11.0f) 
                    {
                        Nodes[i].AddNeighbor(Nodes[j], distance);
                    }
                }
            }
        }
    }

    // encontrar el cubo donde esta un GameObject dado su posicion
    public Node FindCube(Vector3 positionObj)
    {
        int idx_cube = -1;
        float minDistance = float.MaxValue;
        
        for (int i = 0; i < Nodes.Count; i++)
        {
            float distance = Vector3.Distance(positionObj, Nodes[i].getCube().transform.position);
            if (distance < minDistance)
            {
                idx_cube = i;
                minDistance = distance;
            }
        }
        if (idx_cube == -1)
        {
            Debug.LogError("No se encontró el cubo más cercano");
        }
        return Nodes[idx_cube];
    }

    // retornar los vecinos de un nodo
    public List<Connection> getConnections(Node node)
    {
        return node.getNeighbors();
    }

    // visualizar el borde de cada nodo
    public void DrawNodes() {
        foreach (Node node in Nodes)
        {
            // obtener los vertices del cubo
            Vector3[] vertices = node.getCube().GetComponent<MeshFilter>().mesh.vertices;
            // dibujar la cara frontal del cubo
            Vector3 scale = node.getCube().transform.localScale;
            Vector3 offset = new Vector3(scale.x, scale.y, scale.z);

            GameObject line = new GameObject("Line");
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.positionCount = vertices.Length;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = Color.white;
            lr.endColor = Color.white;

            for (int i = 0; i < vertices.Length; i++) {
                lr.SetPosition(i, node.getCube().transform.position + Vector3.Scale(vertices[i], offset));
            }
        }
    }

    // setear los nodos a true que tienen scrolls
    public void setNodesScroll() {
        GameObject[] scrolls = GameObject.FindGameObjectsWithTag("Scroll");

        foreach (GameObject scroll in scrolls)
        {
            Node n = FindCube(scroll.transform.position);
            Nodes[n.getId()].setScroll(true);
        }
    }
}