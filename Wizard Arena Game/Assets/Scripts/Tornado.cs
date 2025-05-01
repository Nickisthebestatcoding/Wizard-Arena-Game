using System.Collections;
using UnityEngine;

public class TornadoProjectile : MonoBehaviour
{
    public float speed = 2f;
    public float knockbackForce = 8f;
    public float knockbackDuration = 0.3f;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                StartCoroutine(ApplyKnockback(rb, knockbackDirection));
            }
        }

        Destroy(gameObject); // Destroy on any collision
    }

    private IEnumerator ApplyKnockback(Rigidbody2D rb, Vector2 direction)
    {
        float timer = 0f;
        while (timer < knockbackDuration)
        {
            rb.velocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }
}
