using UnityEngine;
using System.Collections;

public class PoisonEffect : MonoBehaviour
{
    private Coroutine poisonCoroutine;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void ApplyPoison(float totalDamage, float duration, float tickInterval)
    {
        if (poisonCoroutine != null)
            StopCoroutine(poisonCoroutine);

        poisonCoroutine = StartCoroutine(DoPoisonDamage(totalDamage, duration, tickInterval));
    }

    private IEnumerator DoPoisonDamage(float totalDamage, float duration, float tickInterval)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.green;

        int ticks = Mathf.CeilToInt(duration / tickInterval);
        float damagePerTick = totalDamage / ticks;

        for (int i = 0; i < ticks; i++)
        {
            Health health = GetComponent<Health>();
            if (health != null)
                health.TakeDamage(damagePerTick);

            yield return new WaitForSeconds(tickInterval);
        }

        ResetEffect();
    }

    public void ResetEffect()
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
            poisonCoroutine = null;
        }

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }
}
