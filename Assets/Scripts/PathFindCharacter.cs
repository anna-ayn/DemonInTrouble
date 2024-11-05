using System.Collections.Generic;
using UnityEngine;

public class PathFindCharacter : MonoBehaviour
{
    public GameObject target;
    public Agent character;
    public Graph graph;
    public bool random_target; // si esta deambulando o ya tiene pensado ir a un lugar
    public Node targetNode; // el lugar que quiere ir
    PathFind algorithm;

    List<Node> path = new List<Node>();

    public float maxAcceleration;

    private void Awake()
    {
        character.Initialize();
        graph = new Graph();
        graph.CreateNodes();
        graph.ConnectNodes();
        Debug.Log("Grafo creado para el " + character.gameObject.tag);

        algorithm = new PathFind();
        algorithm.graph = graph;
        algorithm.maxAcceleration = maxAcceleration;    

        if (random_target)
        {
            int randomIndex = Random.Range(0, graph.Nodes.Count);
            targetNode = graph.Nodes[randomIndex];
            Debug.Log("Nodo objetivo seleccionado aleatoriamente");
        } else {
            targetNode = graph.FindCube(target.transform.position);
            Debug.Log("Nodo objetivo: " + targetNode.getCube().name);
        }
    }
   
    
    // Update is called once per frame
    void Update()
    {
        algorithm.character = character.kinematic;
        algorithm.start = graph.FindCube(character.kinematic.position);

        // si ya llego al nodo objetivo aleatorio
        if (path.Count == 0 || Vector3.Distance(character.kinematic.position, targetNode.getCube().transform.position) < 0.1f)
        {
            // conseguir un nuevo nodo aleatorio
            if (random_target)
            {
                int randomIndex = Random.Range(0, graph.Nodes.Count);
                targetNode = graph.Nodes[randomIndex];
            }
        }

        if (!random_target)
        {
            targetNode = graph.FindCube(target.transform.position);
        }

        algorithm.goal = targetNode;
        path = algorithm.pathFindAStar(new Heuristic(algorithm.goal));

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
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
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
                    lineRenderer.startColor = Color.white;
                    lineRenderer.endColor = Color.white;
                }
            }
            // una linea del ultimo nodo al personaje
            GameObject lineObject2 = new GameObject("Line" + lineCharacter);
            lineObject2.tag = "Line" + lineCharacter;
            LineRenderer lineRenderer2 = lineObject2.AddComponent<LineRenderer>();
            lineRenderer2.startWidth = 0.1f;
            lineRenderer2.endWidth = 0.1f;
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
                lineRenderer2.startColor = Color.white;
                lineRenderer2.endColor = Color.white;
            }

            character.steering = algorithm.getSteering(path[path.Count - 1]);
            character.doUpdate(maxAcceleration);

        } 

    }
}