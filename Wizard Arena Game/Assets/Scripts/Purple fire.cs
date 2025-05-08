using UnityEngine;

public class PurpleFire : MonoBehaviour
{
    public float speed = 2f;
    public float totalDamage = 4f;
    public float duration = 4f;
    public float tickInterval = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.CompareTag("Wizard"))
        {
            PurpleFireEffect fireEffect = target.GetComponent<PurpleFireEffect>();
            if (fireEffect != null)
            {
                fireEffect.ApplyEffect(totalDamage, duration, tickInterval);
            }

            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
