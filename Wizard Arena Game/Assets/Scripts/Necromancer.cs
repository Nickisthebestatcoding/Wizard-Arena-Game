using System.Collections;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    [Header("Detection & Movement")]
    public float detectionRadius = 10f;
    public float attackRange = 5f;
    public float moveSpeed = 3f;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootDelay = 1f;

    [Header("Teleportation")]
    public float fadeDuration = 0.5f;
    public float timeBeforeTeleport = 5f;

    [Header("Giant Fireball")]
    public GameObject giantFireballPrefab;
    public Transform giantFireballShootPoint;
    public float giantFireballCooldown = 20f;
    public float giantFireballChance = 0.01f;

    [Header("Circle Attack")]
    public float circleDuration = 3f;
    public float circleRadius = 4f;
    public float circleFireRateMultiplier = 0.3f;
    public float circleAttackCooldown = 15f;
    public float circleChance = 0.005f;

    [Header("Second Phase")]
    public float moveSpeed_Phase2 = 4.5f;
    public float shootDelay_Phase2 = 0.5f;
    public float giantFireballCooldown_Phase2 = 10f;
    public float giantFireballChance_Phase2 = 0.03f;
    public float circleAttackCooldown_Phase2 = 8f;
    public float circleChance_Phase2 = 0.02f;
    public float detectionRadius_Phase2 = 15f;  // NEW
    public float attackRange_Phase2 = 7f;       // NEW
    public float timeBeforeTeleport_Phase2 = 2f; // faster teleporting in phase 2



    private float shootCooldown;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Vector2 randomMoveTarget;
    private bool isTeleporting = false;
    private float timeOutsideAttackRange = 0f;
    private float giantFireballTimer = 0f;
    private bool isCastingGiantFireball = false;
    private bool isCircling = false;
    private float circleCooldownTimer = 0f;
    private float circleAngle = 0f;

    private bool phase2Activated = false;
    private Health health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        randomMoveTarget = transform.position;
        health = GetComponent<Health>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        UpdatePhase();

        if (giantFireballTimer > 0f)
            giantFireballTimer -= Time.deltaTime;

        if (circleCooldownTimer > 0f)
            circleCooldownTimer -= Time.deltaTime;

        if (distanceToPlayer <= detectionRadius)
        {
            RotateTowardsPlayer();

            TryCastGiantFireball();
            TryCircleAttack();

            if (isCastingGiantFireball || isCircling) return;

            if (distanceToPlayer > attackRange)
            {
                timeOutsideAttackRange += Time.deltaTime;

                if (timeOutsideAttackRange >= timeBeforeTeleport && !isTeleporting)
                {
                    StartCoroutine(TeleportIntoAttackRange());
                }

                MoveTowardsPlayer();
            }
            else
            {
                timeOutsideAttackRange = 0f;
                MoveRandomly();
                AttackPlayer();
            }
        }
        else
        {
            timeOutsideAttackRange = 0f;
        }
    }

    void UpdatePhase()
    {
        if (!phase2Activated && health.GetHealthPercent() <= 0.5f)
        {
            phase2Activated = true;
            moveSpeed = moveSpeed_Phase2;
            shootDelay = shootDelay_Phase2;
            giantFireballCooldown = giantFireballCooldown_Phase2;
            giantFireballChance = giantFireballChance_Phase2;
            circleAttackCooldown = circleAttackCooldown_Phase2;
            circleChance = circleChance_Phase2;
            detectionRadius = detectionRadius_Phase2;
            attackRange = attackRange_Phase2;
            timeBeforeTeleport = timeBeforeTeleport_Phase2; // 🆕 set new teleport time

            Debug.Log("Necromancer entered Phase 2!");
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
            PickNewRandomTarget();
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

    void TryCastGiantFireball()
    {
        if (!isCastingGiantFireball && giantFireballTimer <= 0f && Random.value < giantFireballChance)
            StartCoroutine(CastGiantFireball());
    }

    IEnumerator CastGiantFireball()
    {
        isCastingGiantFireball = true;
        yield return new WaitForSeconds(1f);

        if (giantFireballPrefab && giantFireballShootPoint)
            Instantiate(giantFireballPrefab, giantFireballShootPoint.position, giantFireballShootPoint.rotation);

        giantFireballTimer = giantFireballCooldown;
        isCastingGiantFireball = false;
    }

    void TryCircleAttack()
    {
        float chance = phase2Activated ? circleChance_Phase2 : circleChance;
        if (!isCircling && circleCooldownTimer <= 0f && Random.value < chance)
            StartCoroutine(CircleAttack());
    }

    IEnumerator CircleAttack()
    {
        isCircling = true;
        float originalShootDelay = shootDelay;
        shootDelay *= circleFireRateMultiplier;

        float timer = 0f;
        while (timer < circleDuration)
        {
            circleAngle += 360f * Time.deltaTime / circleDuration;
            float rad = circleAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * circleRadius;
            transform.position = player.position + offset;

            AttackPlayer(); // keep attacking
            timer += Time.deltaTime;
            yield return null;
        }

        shootDelay = originalShootDelay;
        isCircling = false;
        circleCooldownTimer = phase2Activated ? circleAttackCooldown_Phase2 : circleAttackCooldown;
    }

    IEnumerator TeleportIntoAttackRange()
    {
        isTeleporting = true;
        yield return StartCoroutine(FadeOut());

        Vector2 offset = Random.insideUnitCircle.normalized * Random.Range(2f, attackRange);
        transform.position = player.position + (Vector3)offset;

        yield return StartCoroutine(FadeIn());
        timeOutsideAttackRange = 0f;
        isTeleporting = false;
    }

    IEnumerator FadeOut()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            SetAlpha(Mathf.Lerp(1f, 0f, t / fadeDuration));
            yield return null;
        }
        SetAlpha(0f);
    }

    IEnumerator FadeIn()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            SetAlpha(Mathf.Lerp(0f, 1f, t / fadeDuration));
            yield return null;
        }
        SetAlpha(1f);
    }

    void SetAlpha(float alpha)
    {
        if (spriteRenderer)
        {
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;
        }
    }
}
