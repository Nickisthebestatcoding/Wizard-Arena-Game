using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Wizard").transform;
        Vector2 direction = (target.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player (you can add more damage logic here)
            Destroy(gameObject);
        }
    }
}
