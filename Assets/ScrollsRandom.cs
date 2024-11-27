using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollsRandom : MonoBehaviour
{
    private Graph graph;
    public Sprite scrollSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        graph = new Graph();
        graph.CreateNodes();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Scroll").Length == 0) {
            // generar un numero aleatorio de scrolls (de 1 a 5) y colocarlos en el juego
            int traps = Random.Range(1, 5);
            Debug.Log("Trampas que se quieren generar: " + traps);
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

                // Order layer to 1000
                scroll.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            }
        }
    }
}
