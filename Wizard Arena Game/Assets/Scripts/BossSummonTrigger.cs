using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSummonTrigger : MonoBehaviour
{
    public GameObject skeletonBossPrefab;
    public Transform spawnPoint;

    public GameObject summoningCircle;
    public GameObject necromancer;

    public GameObject[] borders;  // Assign your border GameObjects in Inspector
    private Health bossHealth;

    public Sprite[] bossHealthSprites;  // Add the health bar sprites for the boss here

    private bool hasSpawned = false;

    public float summonDelay = 2f;

    private void Start()
    {
        if (summoningCircle != null)
            summoningCircle.SetActive(false);

        if (necromancer != null)
            necromancer.SetActive(false);

        SetBordersActive(false);
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

            // Create the health bar UI dynamically
            CreateBossHealthBar(boss);
        }

        yield return new WaitForSeconds(0.5f);

        if (necromancer != null)
            yield return StartCoroutine(FadeOut(necromancer, 1f));

        if (summoningCircle != null)
            yield return StartCoroutine(FadeOut(summoningCircle, 1f));
    }

    // Create the health bar dynamically for the boss
    void CreateBossHealthBar(GameObject boss)
    {
        GameObject healthBarObject = new GameObject("BossHealthBar");
        healthBarObject.transform.SetParent(boss.transform);  // Attach it to the boss
        healthBarObject.transform.localPosition = new Vector3(0f, -1f, 0f);  // Position it below the boss

        // Create an Image component for the health bar
        Image healthBarImage = healthBarObject.AddComponent<Image>();
        healthBarImage.rectTransform.sizeDelta = new Vector2(200f, 20f);  // Adjust size as needed
        healthBarImage.color = Color.red;  // Set a default color for the health bar (optional)

        // Create a BossHealthBar component to manage the health bar's updates
        BossHealthBar bossHealthBar = healthBarObject.AddComponent<BossHealthBar>();
        bossHealthBar.SetHealthBarImage(healthBarImage);  // Set the health bar image

        // Set the sprites for the health bar
        bossHealthBar.healthSprites = bossHealthSprites;

        // Update the health bar at full health initially
        bossHealthBar.UpdateHealthBar(1f);
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