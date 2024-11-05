using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int id; // Identificador único del nodo
    private GameObject Cube; // Referencia al cubo en el mundo del juego
    private List<Connection> Neighbors; // Lista de conexiones a nodos vecinos

    public Node(int id, GameObject cube)
    {
        this.id = id;
        this.Cube = cube;
        this.Neighbors = new List<Connection>();
    }

    public void AddNeighbor(Node neighbor, float cost)
    {
        Neighbors.Add(new Connection(this, neighbor, cost));
    }

    public int getId() {
        return id;
    }

    public GameObject getCube() {
        return Cube;
    }

    public List<Connection> getNeighbors() {
        return Neighbors;
    }
}