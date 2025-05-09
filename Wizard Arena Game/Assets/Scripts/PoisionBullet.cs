using UnityEngine;

public class PoisonBullet : MonoBehaviour
{
    public float speed = 2f;
    public float totalDamage = 4f; // Total damage over time
    public float duration = 4f;    // How long the poison lasts
    public float tickInterval = 1f; // How often damage is applied (in seconds)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;  // Move forward
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.CompareTag("Wizard"))
        {
            PoisonEffect poison = target.GetComponent<PoisonEffect>();
            if (poison != null)
            {
                poison.ApplyPoison(totalDamage, duration, tickInterval);
            }

            Destroy(gameObject);
            return;
        }

        Destroy(gameObject); // Destroy if it hits anything else
    }
}

