using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossSummonTrigger : MonoBehaviour
{
    public GameObject skeletonBossPrefab;
    public Transform spawnPoint;

    public GameObject summoningCircle;
    public GameObject necromancer;

    public GameObject[] borders;  // Assign your border GameObjects in Inspector
    private Health bossHealth;

    public GameObject bossHealthBarObject; // Drag the boss health bar GameObject here

    private bool hasSpawned = false;

    public float summonDelay = 2f;
    public float screenShakeDuration = 0.5f;
    public float screenShakeIntensity = 0.2f;

    private void Start()
    {
        if (summoningCircle != null)
            summoningCircle.SetActive(false);

        if (necromancer != null)
            necromancer.SetActive(false);

        SetBordersActive(false);

        if (bossHealthBarObject != null)
            bossHealthBarObject.SetActive(false); // Hide boss bar at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Wizard"))
        {
            hasSpawned = true;
            SetBordersActive(true);
            StartCoroutine(SummoningSequence());
        }
    }

    private void SetBordersActive(bool isActive)
    {
        foreach (GameObject border in borders)
        {
            if (border != null)
                border.SetActive(isActive);
        }
    }

    public void OpenBorders()
    {
        Debug.Log("Opening borders.");
        SetBordersActive(false);
    }

    IEnumerator SummoningSequence()
    {
        if (summoningCircle != null)
        {
            summoningCircle.SetActive(true);
            StartCoroutine(FadeIn(summoningCircle, 1f));
        }

        if (necromancer != null)
        {
            necromancer.SetActive(true);
            StartCoroutine(FadeIn(necromancer, 1f));
        }

        yield return new WaitForSeconds(1.5f);

        if (necromancer != null)
        {
            Animator anim = necromancer.GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("Summon");
        }

        yield return new WaitForSeconds(summonDelay);

        if (skeletonBossPrefab != null && spawnPoint != null)
        {
            GameObject boss = Instantiate(skeletonBossPrefab, spawnPoint.position, spawnPoint.rotation);
            bossHealth = boss.GetComponent<Health>();

            if (bossHealthBarObject != null)
                bossHealthBarObject.SetActive(true); // Show the boss bar

            BossHealthBar bar = bossHealthBarObject.GetComponent<BossHealthBar>();
            if (bar != null)
            {
                bossHealth.bossHealthBarUI = bar;
                bar.UpdateHealthBar(1f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        if (necromancer != null)
            StartCoroutine(FadeOut(necromancer, 1f));

        yield return new WaitForSeconds(0.5f);

        if (summoningCircle != null)
            summoningCircle.SetActive(false);
    }

    IEnumerator FadeIn(GameObject obj, float duration)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color startColor = sr.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            sr.color = Color.Lerp(new Color(startColor.r, startColor.g, startColor.b, 0f), endColor, t / duration);
            yield return null;
        }

        sr.color = endColor;
    }

    IEnumerator FadeOut(GameObject obj, float duration)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color startColor = sr.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            sr.color = Color.Lerp(startColor, endColor, t / duration);
            yield return null;
        }

        sr.color = endColor;
        obj.SetActive(false);
    }
}