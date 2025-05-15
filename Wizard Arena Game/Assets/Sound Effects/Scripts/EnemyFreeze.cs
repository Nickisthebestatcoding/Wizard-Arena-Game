using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isFrozen = false;
    private bool wasEnragedColor = false;

    private KnightBehavior knightBehavior;
    private EnemyWizardAI enemyWizardAI;
    private SkeletonBoss skeletonBoss;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        knightBehavior = GetComponent<KnightBehavior>();
        enemyWizardAI = GetComponent<EnemyWizardAI>();
        skeletonBoss = GetComponent<SkeletonBoss>();
    }

    public void Freeze(float duration)
    {
        if (knightBehavior != null)
            knightBehavior.enabled = false;
        if (enemyWizardAI != null)
            enemyWizardAI.enabled = false;
        if (skeletonBoss != null)
            skeletonBoss.enabled = false;

        // Track if the current color is already the enraged color
        if (skeletonBoss != null && sr.color == Color.red)  // Assuming red is the enraged color
        {
            wasEnragedColor = true;
        }

        sr.color = Color.cyan; // Freeze color
        isFrozen = true;

        CancelInvoke(nameof(Unfreeze));
        Invoke(nameof(Unfreeze), duration);
    }

    void Unfreeze()
    {
        if (knightBehavior != null)
            knightBehavior.enabled = true;
        if (enemyWizardAI != null)
            enemyWizardAI.enabled = true;
        if (skeletonBoss != null)
            skeletonBoss.enabled = true;

        // Restore the correct color based on enraged state
        if (wasEnragedColor && skeletonBoss != null)
        {
            sr.color = Color.red;  // Restore the enraged color (assuming red)
        }
        else
        {
            sr.color = originalColor;
        }

        isFrozen = false;
        wasEnragedColor = false;
    }

    public void ResetEffect()
    {
        if (isFrozen)
        {
            CancelInvoke(nameof(Unfreeze));
            Unfreeze();
        }
    }
}
