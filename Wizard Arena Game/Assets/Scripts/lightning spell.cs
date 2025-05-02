using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    public float damage = 3f;
    public float duration = 0.3f; // How long it lasts
    public LayerMask enemyLayer;
    public Animator animator;

    private bool hasDealtDamage = false;

    void Start()
    {
        animator.SetTrigger("Activate");
        Invoke(nameof(DestroySelf), duration);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Called by animation event or during active window
    void DealDamage()
    {
        if (hasDealtDamage) return;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Wizard")) continue; // Skip wizard

            Health health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        hasDealtDamage = true;
    }
}
