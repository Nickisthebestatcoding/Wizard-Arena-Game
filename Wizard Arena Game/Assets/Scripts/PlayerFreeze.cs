using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    private WizardScript wizardScript;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        wizardScript = GetComponent<WizardScript>();
    }

    public void Freeze(float duration)
    {
        if (wizardScript != null)
            wizardScript.canMove = false;

        sr.color = Color.cyan;
        Invoke(nameof(Unfreeze), duration);
    }

    void Unfreeze()
    {
        if (wizardScript != null)
            wizardScript.canMove = true;

        sr.color = originalColor;
    }
}
