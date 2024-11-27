using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnemyController : MonoBehaviour
{
    public Sprite SpriteCauldron; 
    public Sprite SpriteGhostCrying;
    private SpriteRenderer RendererCauldron;
    private SpriteRenderer RendererGhostCrying;

    void Awake()
    {
        GameObject cauldron = new GameObject("Cauldron");
        cauldron.transform.parent = transform;
        RendererCauldron = cauldron.AddComponent<SpriteRenderer>();

        GameObject ghostCrying = new GameObject("GhostCrying");
        ghostCrying.transform.parent = transform;
        RendererGhostCrying = ghostCrying.AddComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Set the sprite for the SpriteRenderer
        if (SpriteCauldron != null && RendererCauldron != null)
        {
            RendererCauldron.sprite = SpriteCauldron;
            // Optionally adjust position and scale here
            RendererCauldron.transform.localPosition = new Vector3(0, -0.5f, 0); 
            RendererCauldron.transform.localScale = new Vector3(0.05f, 0.05f, 1); 

            // Order layer to 5100
            RendererCauldron.sortingOrder = 5100;
        }
        else
        {
            Debug.LogWarning(" sprite not assigned or SpriteRenderer is missing!");
        }

        if (SpriteGhostCrying != null && RendererGhostCrying != null)
        {
            RendererGhostCrying.sprite = SpriteGhostCrying;
            // Optionally adjust position and scale here
            RendererGhostCrying.transform.localPosition = new Vector3(0, 0.7f, 0); 
            RendererGhostCrying.transform.localScale = new Vector3(0.4f, 0.4f, 1); 

            // Order layer to 6000
            RendererGhostCrying.sortingOrder = 6000;
        }
        else
        {
            Debug.LogWarning(" sprite not assigned or SpriteRenderer is missing!");
        }
    }

    public void Show()
    {
        if (RendererCauldron != null)
        {
            RendererCauldron.enabled = true; 
        }

        if (RendererGhostCrying != null)
        {
            RendererGhostCrying.enabled = true; 
        }


    }

    public void Hide()
    {
        if (RendererCauldron != null)
        {
            RendererCauldron.enabled = false; 
        }

        if (RendererGhostCrying != null)
        {
            RendererGhostCrying.enabled = false;
        }
    }
}
