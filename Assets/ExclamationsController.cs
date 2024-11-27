using UnityEngine;

public class ExclamationsController : MonoBehaviour
{
    public Sprite exclamations;
    private SpriteRenderer exclamationsRenderer;

    void Awake()
    {
        GameObject exclamationsObject = new GameObject("Exclamations");
        
        exclamationsObject.transform.parent = transform;

        exclamationsRenderer = exclamationsObject.AddComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Set the sprite for the SpriteRenderer
        if (exclamations != null && exclamationsRenderer != null)
        {
            exclamationsRenderer.sprite = exclamations;
            // Optionally adjust position and scale here
            exclamationsRenderer.transform.localPosition = new Vector3(0, 0.25f, 0); 
            exclamationsRenderer.transform.localScale = new Vector3(0.07f, 0.1f, 1); 

            // Order layer to 1000
            exclamationsRenderer.sortingOrder = 1000;
        }
        else
        {
            Debug.LogWarning("exclamations sprite not assigned or SpriteRenderer is missing!");
        }
    }

    public void ShowExclamations()
    {
        if (exclamationsRenderer != null)
        {
            exclamationsRenderer.enabled = true; // Make sure the Exclamations is visible
        }
    }

    public void HideExclamations()
    {
        if (exclamationsRenderer != null)
        {
            exclamationsRenderer.enabled = false; // Hide the Exclamations
        }
    }
}