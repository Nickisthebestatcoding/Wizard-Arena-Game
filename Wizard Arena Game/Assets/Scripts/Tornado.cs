using System.Collections;
using UnityEngine;

public class TornadoProjectile : MonoBehaviour
{
    public float speed = 2f;
    public float pushForce = 10f;
    public float stopDelay = 0.2f;

    private void Update()
    {
        // Move forward (in facing direction)
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 pushDirection = (other.transform.position - transform.position).normalized;
                rb.velocity = pushDirection * pushForce;

                StartCoroutine(StopMovement(rb, stopDelay));
            }
        }

        // Destroy tornado on any collision
        Destroy(gameObject);
    }

    private IEnumerator StopMovement(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rb != null)
            rb.velocity = Vector2.zero;
    }
}
