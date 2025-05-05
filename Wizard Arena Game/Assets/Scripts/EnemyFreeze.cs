using UnityEngine;


public class EnemyFreeze : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isFrozen = false;
    private KnightBehavior knightBehavior;
    private EnemyWizardAI enemyWizardAI;
    private SkeletonBoss skeletonBoss;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        // Get the respective scripts based on the type of enemy
        knightBehavior = GetComponent<KnightBehavior>();
        enemyWizardAI = GetComponent<EnemyWizardAI>();
        skeletonBoss = GetComponent<SkeletonBoss>();
    }

    public void Freeze(float duration)
    {
        // Disable behavior of specific enemies if they exist
        if (knightBehavior != null)
            knightBehavior.enabled = false;
        if (enemyWizardAI != null)
            enemyWizardAI.enabled = false;
        if (skeletonBoss != null)
            skeletonBoss.enabled = false;

        // Change color to indicate freeze state
        sr.color = Color.cyan;
        isFrozen = true;

        // Ensure only one unfreeze is called after the given duration
        CancelInvoke(nameof(Unfreeze));
        Invoke(nameof(Unfreeze), duration);
    }

    void Unfreeze()
    {
        // Re-enable behavior after freeze period
        if (knightBehavior != null)
            knightBehavior.enabled = true;
        if (enemyWizardAI != null)
            enemyWizardAI.enabled = true;
        if (skeletonBoss != null)
            skeletonBoss.enabled = true;

        // Revert sprite color back to original
        sr.color = originalColor;
        isFrozen = false;
    }

    // Allows resetting the freeze effect manually if needed
    public void ResetEffect()
    {
        if (isFrozen)
        {
            CancelInvoke(nameof(Unfreeze));
            Unfreeze();  // Immediately unfreeze the enemy
        }
    }
}
