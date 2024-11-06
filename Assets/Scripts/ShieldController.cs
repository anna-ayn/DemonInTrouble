using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Sprite shieldSprite; // Assign this in the Inspector
    private SpriteRenderer shieldRenderer;

    void Awake()
    {
        // Create a new GameObject for the shield
        GameObject shieldObject = new GameObject("Shield");
        
        // Set it as a child of this GameObject
        shieldObject.transform.parent = transform;

        // Add a SpriteRenderer component to the shield object
        shieldRenderer = shieldObject.AddComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Set the sprite for the SpriteRenderer
        if (shieldSprite != null && shieldRenderer != null)
        {
            shieldRenderer.sprite = shieldSprite;
            // Optionally adjust position and scale here
            shieldRenderer.transform.localPosition = new Vector3(0, 0, 0); 
            shieldRenderer.transform.localScale = new Vector3(0.1f, 0.2f, 1); 

            // Order layer to 1000
            shieldRenderer.sortingOrder = 1000;
        }
        else
        {
            Debug.LogWarning("Shield sprite not assigned or SpriteRenderer is missing!");
        }
    }

    public void ShowShield()
    {
        if (shieldRenderer != null)
        {
            shieldRenderer.enabled = true; // Make sure the shield is visible
        }
    }

    public void HideShield()
    {
        if (shieldRenderer != null)
        {
            shieldRenderer.enabled = false; // Hide the shield
        }
    }
}