using UnityEngine;
using System.Collections;

public class PurpleFireEffect : MonoBehaviour
{
    private Coroutine effectCoroutine;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private readonly Color purpleColor = new Color(0.6f, 0.1f, 0.8f); // Custom purple

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void ApplyEffect(float totalDamage, float duration, float tickInterval)
    {
        if (effectCoroutine != null)
            StopCoroutine(effectCoroutine);

        effectCoroutine = StartCoroutine(DoEffectDamage(totalDamage, duration, tickInterval));
    }

    private IEnumerator DoEffectDamage(float totalDamage, float duration, float tickInterval)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = purpleColor;

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
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            effectCoroutine = null;
        }

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }
}
