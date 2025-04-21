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
        if (summoningCircle != null)
            summoningCircle.SetActive(true);

        if (necromancer != null)
        {
            necromancer.SetActive(true);
            Animator anim = necromancer.GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("Summon");
        }

        yield return new WaitForSeconds(summonDelay);

        if (necromancer != null)
            necromancer.SetActive(false);


        if (skeletonBossPrefab != null && spawnPoint != null)
        {
            Instantiate(skeletonBossPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        yield return new WaitForSeconds(1f);

        if (summoningCircle != null)
            summoningCircle.SetActive(false);
    }
}