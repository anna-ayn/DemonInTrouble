using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollsRandom : MonoBehaviour
{
    private Graph graph;
    public Sprite scrollSprite;
    private int n_scrolls;
    public int max_scrolls = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        graph = new Graph();
        graph.CreateNodes();
        n_scrolls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // actualizar el numero de scrolls en el juego
        if (GameObject.FindGameObjectsWithTag("Scroll").Length < n_scrolls) {
            n_scrolls = GameObject.FindGameObjectsWithTag("Scroll").Length;
            
        }

        if (GameObject.FindGameObjectsWithTag("Scroll").Length == 0) {
            int traps = Random.Range(1, max_scrolls);
            Debug.Log("Trampas que se quieren generar: " + traps);
            n_scrolls = traps;
            for (int i = 0; i < traps; i++) {
                GameObject scroll = new GameObject("Scroll");
                scroll.tag = "Scroll";
                scroll.AddComponent<SpriteRenderer>();
                scroll.GetComponent<SpriteRenderer>().sprite = scrollSprite;

                GameObject scrollParent = GameObject.FindWithTag("Traps");
                if (scrollParent != null)
                {
                    scroll.transform.parent = scrollParent.transform;
                }

                // colocar el scroll en una posicion aleatoria
                int randomIndex = Random.Range(0, graph.Nodes.Count);
                scroll.transform.position = graph.Nodes[randomIndex].getCube().transform.position;
                scroll.transform.localScale = new Vector3(10f, 10f, 1);

                // setear de que el nodo tiene un scroll
                graph.Nodes[randomIndex].setScroll(true);

                // Order layer to 1000
                scroll.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            }
        }
    }
}
