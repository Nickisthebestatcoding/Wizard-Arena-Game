using UnityEngine;


public class EnemyFreeze : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isFrozen = false;
    private knightBehavior KnightBehavior;
    private EnemyWizardAI enemyWizardAI;
    private SkeletonBoss skeletonBoss;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        // Get the respective scripts based on the type of enemy
        KnightBehavior = GetComponent<knightBehavior>();
        enemyWizardAI = GetComponent<EnemyWizardAI>();
        skeletonBoss = GetComponent<SkeletonBoss>();
    }

    public void Freeze(float duration)
    {
        if (KnightBehavior != null)
            KnightBehavior.enabled = false;  // Disable movement and behavior for knight
        if (enemyWizardAI != null)
            enemyWizardAI.enabled = false;  // Disable movement and behavior for enemy wizard
        if (skeletonBoss != null)
            skeletonBoss.enabled = false;  // Disable movement and behavior for skeleton boss

        sr.color = Color.cyan;  // Change color to indicate freeze
        isFrozen = true;

        CancelInvoke(nameof(Unfreeze));
        Invoke(nameof(Unfreeze), duration);
    }

    void Unfreeze()
    {
        if (KnightBehavior != null)
            KnightBehavior.enabled = true;  // Re-enable knight behavior
        if (enemyWizardAI != null)
            enemyWizardAI.enabled = true;  // Re-enable enemy wizard behavior
        if (skeletonBoss != null)
            skeletonBoss.enabled = true;  // Re-enable skeleton boss behavior

        sr.color = originalColor;  // Revert color
        isFrozen = false;
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
