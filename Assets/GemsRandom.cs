using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsRandom : MonoBehaviour
{
    private Graph graph;
    public Sprite[] spritesGems;
    // Start is called before the first frame update
    void Start()
    {
        graph = new Graph();
        graph.CreateNodes();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Gem").Length == 0) {
            Debug.Log("No hay gemas");
            // generar un numero aleatorio de gemas (de 1 a 15) y colocarlos en el juego
            int gemas = Random.Range(1, 15);
            Debug.Log("Gemas que se quieren generar: " + gemas);
            for (int i = 0; i < gemas; i++) {
                int gemColor = Random.Range(1, spritesGems.Length);

                // crear una nueva gema
                GameObject gem = new GameObject("Gem");
                gem.tag = "Gem";
                gem.AddComponent<SpriteRenderer>();
                gem.GetComponent<SpriteRenderer>().sprite = spritesGems[gemColor];
                
                // colocar la gema dentro de un GameObject con tag "Gems"
                GameObject gemsParent = GameObject.FindWithTag("Gems");
                if (gemsParent != null)
                {
                    gem.transform.parent = gemsParent.transform;
                }

                // colocar la gema en una posicion aleatoria
                int randomIndex = Random.Range(0, graph.Nodes.Count);
                gem.transform.position = graph.Nodes[randomIndex].getCube().transform.position;
                gem.transform.localScale = new Vector3(30f, 30f, 1); 

                // Order layer to 1000
                gem.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            }
        }
    }
}
