using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonTrigger : MonoBehaviour
{
    public GameObject skeletonBossPrefab;
    public Transform spawnPoint;

    public GameObject summoningCircle;
    public GameObject necromancer;

    public float summonDelay = 2f;
    public float screenShakeDuration = 0.5f;
    public float screenShakeIntensity = 0.2f;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Wizard"))
        {
            hasSpawned = true;
            StartCoroutine(SummoningSequence());
        }
    }

    IEnumerator SummoningSequence()
    {
        // 1. Show summoning circle
        if (summoningCircle != null)
            summoningCircle.SetActive(true);

        // 2. Show necromancer & play summon animation
        if (necromancer != null)
        {
            necromancer.SetActive(true);

            Animator necroAnim = necromancer.GetComponent<Animator>();
            if (necroAnim != null)
                necroAnim.SetTrigger("Summon");
        }

        // 3. Wait for the summoning to complete
        yield return new WaitForSeconds(summonDelay);

        // 4. Hide necromancer
        if (necromancer != null)
            necromancer.SetActive(false);

        // 5. Screen shake
        if (CameraShaker.Instance != null)
            CameraShaker.Instance.Shake(screenShakeDuration, screenShakeIntensity);

        // 6. Spawn boss
        if (skeletonBossPrefab != null && spawnPoint != null)
        {
            Instantiate(skeletonBossPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Skeleton Boss Spawned!");
        }

        // 7. Optionally hide the summoning circle after a moment
        yield return new WaitForSeconds(1f);
        if (summoningCircle != null)
            summoningCircle.SetActive(false);
    }
}