using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFindCharacter : MonoBehaviour
{
    public GameObject graphObject;
    public GameObject target;
    public Agent character;
    public Graph graph;
    public bool random_target; // si esta deambulando o ya tiene pensado ir a un lugar
    public Node targetNode; // el lugar que quiere ir
    public Node beforeTargetNode; // el lugar que queria ir antes de cambiar de objetivo
    PathFind algorithm;

    public List<Node> path;

    public float maxAcceleration;

    private void Awake()
    {
        // esperar 1 segundo para inicializar el grafo
        character.Initialize();

        graph = new Graph();
        graph.CreateNodes();
        graph.ConnectNodes();
        graph.setNodesScroll();

        algorithm = new PathFind();
        algorithm.graph = graph;
        algorithm.maxAcceleration = maxAcceleration;    
    }
   
    
    // Update is called once per frame
    void Update()
    {
        algorithm.character = character.kinematic;
        algorithm.start = graph.FindCube(character.kinematic.position);

        if (path == null) {
            if (!random_target && target != null)
            {
                targetNode = graph.FindCube(target.transform.position);
            }

            algorithm.goal = targetNode;
            path = algorithm.pathFindAStar(new Heuristic(algorithm.goal));

            beforeTargetNode = targetNode;
        }

        if (beforeTargetNode != null && beforeTargetNode != targetNode) {
            path = algorithm.pathFindAStar(new Heuristic(algorithm.goal));
        }

        if (path.Count > 0) {
            
            string lineCharacter = "Unknown"; 
            if (character.gameObject.tag == "Enemy")
            {
                lineCharacter = "Enemy";
            } else if (character.gameObject.tag == "Player") {
                lineCharacter = "Player";
            } else if (character.gameObject.tag == "Pet") {
                lineCharacter = "Pet";
            }  
                
            GameObject[] lines = GameObject.FindGameObjectsWithTag("Line" + lineCharacter);
            foreach (GameObject line in lines)
            {
                Destroy(line);
            }

            for (int j = 0; j < path.Count - 1; j++)
            {
                GameObject lineObject = new GameObject("Line" + lineCharacter);
                lineObject.tag = "Line" + lineCharacter;
                LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.3f;
                lineRenderer.endWidth = 0.3f;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, path[j].getCube().transform.position);
                lineRenderer.SetPosition(1, path[j + 1].getCube().transform.position);
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                if (character.gameObject.tag == "Player")
                {
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = Color.red;
                } else if (character.gameObject.tag == "Enemy") {
                    lineRenderer.startColor = Color.blue;
                    lineRenderer.endColor = Color.blue;
                } else if (character.gameObject.tag == "Pet") {
                    lineRenderer.startColor = Color.black;
                    lineRenderer.endColor = Color.black;
                }
            }
            // una linea del ultimo nodo al personaje
            GameObject lineObject2 = new GameObject("Line" + lineCharacter);
            lineObject2.tag = "Line" + lineCharacter;
            LineRenderer lineRenderer2 = lineObject2.AddComponent<LineRenderer>();
            lineRenderer2.startWidth = 0.3f;
            lineRenderer2.endWidth = 0.3f;
            lineRenderer2.positionCount = 2;
            lineRenderer2.SetPosition(0, path[path.Count - 1].getCube().transform.position);
            lineRenderer2.SetPosition(1, character.kinematic.position);
            lineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
            if (character.gameObject.tag == "Player")
            {
                lineRenderer2.startColor = Color.red;
                lineRenderer2.endColor = Color.red;
            } else if (character.gameObject.tag == "Enemy") {
                lineRenderer2.startColor = Color.blue;
                lineRenderer2.endColor = Color.blue;
            }
             else if (character.gameObject.tag == "Pet") {
                lineRenderer2.startColor = Color.black;
                lineRenderer2.endColor = Color.black;
            }

            character.steering = algorithm.getSteering(path[path.Count - 1]);
            character.doUpdate(maxAcceleration);

        } 

    }
}