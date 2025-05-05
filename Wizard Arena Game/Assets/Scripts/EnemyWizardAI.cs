using System.Collections;
using UnityEngine;

public class EnemyWizardAI : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float attackRange = 5f;
    public float moveSpeed = 3f;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootDelay = 1f;

    private Transform player;
    private float shootCooldown;
    private Vector2 randomMoveTarget;

    private Coroutine knockbackRoutine;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        randomMoveTarget = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            RotateTowardsPlayer();

            if (distanceToPlayer <= attackRange)
            {
                MoveRandomly();
                AttackPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void MoveRandomly()
    {
        transform.position = Vector2.MoveTowards(transform.position, randomMoveTarget, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, randomMoveTarget) < 0.2f)
        {
            PickNewRandomTarget();
        }
    }

    void PickNewRandomTarget()
    {
        Vector2 offset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        randomMoveTarget = (Vector2)transform.position + offset;
    }

    void AttackPlayer()
    {
        if (shootCooldown <= 0f)
        {
            ShootProjectile();
            shootCooldown = shootDelay;
        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    void ShootProjectile()
    {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }

    public void ApplyPush(Vector2 force, float duration)
    {
        if (knockbackRoutine != null)
            StopCoroutine(knockbackRoutine);

        knockbackRoutine = StartCoroutine(ApplyKnockback(force, duration));
    }

    private IEnumerator ApplyKnockback(Vector2 force, float duration)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) yield break;

        float timer = 0f;
        while (timer < duration)
        {
            rb.velocity = force;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }
}
