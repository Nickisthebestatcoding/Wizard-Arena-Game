using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonTrigger : MonoBehaviour
{
    public GameObject skeletonBossPrefab;
    public Transform spawnPoint;

    public GameObject summoningCircle;
    public GameObject necromancer;

    public GameObject[] borders;  // Reference to the border objects (assign in Inspector)

    public float summonDelay = 2f;
    public float screenShakeDuration = 0.5f;
    public float screenShakeIntensity = 0.2f;

    private bool hasSpawned = false;

    private void Start()
    {
        if (summoningCircle != null)
        {
            summoningCircle.SetActive(false); // So we manually enable with fade later
        }

        if (necromancer != null)
        {
            necromancer.SetActive(false);
        }

        // Hide the borders initially (in case they are not already set to inactive in the Inspector)
        SetBordersActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Wizard"))
        {
            hasSpawned = true;
            SetBordersActive(true); // Show borders when the player touches the GameObject
            StartCoroutine(SummoningSequence());
        }
    }

    // Set borders active or inactive
    private void SetBordersActive(bool isActive)
    {
        foreach (GameObject border in borders)
        {
            if (border != null)
            {
                border.SetActive(isActive); // Activate or deactivate each border
            }
        }
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

        yield return new WaitForSeconds(1.5f); // wait for fade-in to finish

        if (necromancer != null)
        {
            Animator anim = necromancer.GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("Summon");
        }

        yield return new WaitForSeconds(summonDelay);

        if (skeletonBossPrefab != null && spawnPoint != null)
        {
            Instantiate(skeletonBossPrefab, spawnPoint.position, spawnPoint.rotation);
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
