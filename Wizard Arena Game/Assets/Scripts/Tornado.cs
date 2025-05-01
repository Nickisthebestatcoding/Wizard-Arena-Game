using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float speed = 1f;
    public float pushForce = 10f;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Push back the player if it's the Wizard
        if (other.CompareTag("Wizard"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 pushDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }

        // Destroy on any contact
        Destroy(gameObject);
    }
}
