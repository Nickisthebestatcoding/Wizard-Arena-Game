using UnityEngine;

public class HighlightOnTrigger : MonoBehaviour
{
    public Color highlightColor = Color.blue;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            spriteRenderer.color = highlightColor;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            spriteRenderer.color = originalColor;
        }
    }
}