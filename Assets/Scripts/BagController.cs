using UnityEngine;

public class BagController : MonoBehaviour
{
    public Sprite bagSprite; // Assign this in the Inspector
    private SpriteRenderer bagRenderer;

    void Awake()
    {
        // Create a new GameObject for the bag
        GameObject bagObject = new GameObject("Bag");
        
        // Set it as a child of this GameObject
        bagObject.transform.parent = transform;

        // Add a SpriteRenderer component to the bag object
        bagRenderer = bagObject.AddComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Set the sprite for the SpriteRenderer
        if (bagSprite != null && bagRenderer != null)
        {
            bagRenderer.sprite = bagSprite;
            // Optionally adjust position and scale here
            bagRenderer.transform.localPosition = new Vector3(0, 0, 0); 
            bagRenderer.transform.localScale = new Vector3(0.1f, 0.2f, 1); 

            // Order layer to 1000
            bagRenderer.sortingOrder = 1000;
        }
        else
        {
            Debug.LogWarning("bag sprite not assigned or SpriteRenderer is missing!");
        }
    }

    public void Showbag()
    {
        if (bagRenderer != null)
        {
            bagRenderer.enabled = true; // Make sure the bag is visible
        }
    }

    public void Hidebag()
    {
        if (bagRenderer != null)
        {
            bagRenderer.enabled = false; // Hide the bag
        }
    }

    public bool isShowingABag() 
    {
        return bagRenderer.enabled;
    }
}