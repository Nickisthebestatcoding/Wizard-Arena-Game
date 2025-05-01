using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float speed = 2f;
    public float freezeDuration = 2f;

    void Update()
    {
        // Move forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wizard"))
        {
            PlayerFreeze freeze = collision.GetComponent<PlayerFreeze>();
            if (freeze != null)
            {
                freeze.Freeze(freezeDuration);
            }

            Destroy(gameObject);
        }
    }
}
