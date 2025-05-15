using UnityEngine;

public class HighlightOnProximity : MonoBehaviour
{
    public float detectionRadius = 5f;  // Distance at which the object will highlight
    public Color highlightColor = Color.red; // Color for highlighting
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer of the object to change its color
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;  // Save the original color
        }
    }

    void Update()
    {
        // Find the player in the scene
        GameObject wizard = GameObject.FindWithTag("Wizard");

        if (wizard != null)
        {
            // Calculate the distance between the object and the player
            float distance = Vector2.Distance(transform.position, wizard.transform.position);

            if (distance < detectionRadius)
            {
                Highlight(true);  // Highlight the object
            }
            else
            {
                Highlight(false); // Revert back to original state
            }
        }
    }

    void Highlight(bool isHighlighting)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = isHighlighting ? highlightColor : originalColor;
        }
    }
}