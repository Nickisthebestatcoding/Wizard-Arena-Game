using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float speed = 2f;
    public float freezeDuration = 2f;
    public float damage = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Make the bullet move in the direction it is facing
        rb.velocity = transform.up * speed;  // Use transform.up (local forward direction)
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wizard"))
        {
            PlayerFreeze freeze = collision.GetComponent<PlayerFreeze>();
            if (freeze != null)
                freeze.Freeze(freezeDuration);

            Health health = collision.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
