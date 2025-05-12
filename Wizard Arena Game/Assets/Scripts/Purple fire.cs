using UnityEngine;

public class PurpleFire : MonoBehaviour
{
    [Header("Phase 1")]
    public float speed = 2f;
    public float totalDamage = 4f;
    public float duration = 4f;
    public float tickInterval = 1f;

    [Header("Phase 2")]
    public float speed_Phase2 = 3f;
    public float totalDamage_Phase2 = 6f;
    public float duration_Phase2 = 5f;
    public float tickInterval_Phase2 = 0.5f;

    private Rigidbody2D rb;
    private bool isPhase2 = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * (isPhase2 ? speed_Phase2 : speed);
    }

    public void SetPhase2(bool value)
    {
        isPhase2 = value;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.CompareTag("Wizard"))
        {
            PurpleFireEffect fireEffect = target.GetComponent<PurpleFireEffect>();
            if (fireEffect != null)
            {
                if (isPhase2)
                {
                    fireEffect.ApplyEffect(totalDamage_Phase2, duration_Phase2, tickInterval_Phase2);
                }
                else
                {
                    fireEffect.ApplyEffect(totalDamage, duration, tickInterval);
                }
            }

            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
