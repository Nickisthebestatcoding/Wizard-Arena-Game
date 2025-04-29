using UnityEngine;

public class HighlightOnProximity : MonoBehaviour
{
    public float highlightRadius = 5f;
    public Color highlightColor = Color.yellow;

    private Transform wizard;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // Try to find the player by tag
        GameObject wizardObject = GameObject.FindGameObjectWithTag("Player");
        if (wizardObject != null)
        {
            wizard = wizardObject.transform;
        }
        else
        {
            Debug.LogWarning("Wizard not found. Make sure your player has the 'Wizard' tag.");
        }
    }

    void Update()
    {
        if (wizard == null) return;

        float distance = Vector2.Distance(wizard.position, transform.position);
        spriteRenderer.color = distance <= highlightRadius ? highlightColor : originalColor;
    }
}
