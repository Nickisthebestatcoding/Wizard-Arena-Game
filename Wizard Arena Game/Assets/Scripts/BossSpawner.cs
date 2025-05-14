using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject skeletonBossPrefab;
    public Transform spawnPoint;

    public GameObject summoningCircle;
    public GameObject necromancer;
    public GameObject[] borders;

    public float summonDelay = 2f;
    public float screenShakeDuration = 0.5f;
    public float screenShakeIntensity = 0.2f;

    [Header("Camera Zoom")]
    public Camera mainCamera;
    public float zoomDuringSummon = 4f;
    public float zoomDuringFight = 6f;
    public float zoomDefault = 8f;
    public float zoomSpeed = 2f;

    private Health bossHealth;
    private bool hasSpawned = false;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (summoningCircle != null) summoningCircle.SetActive(false);
        if (necromancer != null) necromancer.SetActive(false);
        SetBordersActive(false);
    }

    public void TriggerSummon()
    {
        if (!hasSpawned)
        {
            hasSpawned = true;
            SetBordersActive(true);
            StartCoroutine(SummoningSequence());
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

        yield return StartCoroutine(ZoomCamera(zoomDuringSummon));
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
        }

        yield return new WaitForSeconds(0.5f);

        if (necromancer != null)
            StartCoroutine(FadeOut(necromancer, 1f));

        yield return new WaitForSeconds(0.5f);

        if (summoningCircle != null)
            summoningCircle.SetActive(false);

        yield return StartCoroutine(ZoomCamera(zoomDuringFight));
    }

    public void OnWizardDeath()
    {
        Debug.Log("Wizard died. Resetting boss and necromancer.");
        ResetBossState();
    }

    public void ResetBossState()
    {
        hasSpawned = false;

        if (bossHealth != null && bossHealth.gameObject != null)
        {
            Destroy(bossHealth.gameObject);
            bossHealth = null;
        }

        if (necromancer != null)
        {
            StopAllCoroutines();
            necromancer.SetActive(false);
        }

        if (summoningCircle != null)
            summoningCircle.SetActive(false);

        SetBordersActive(false);
        StartCoroutine(ZoomCamera(zoomDefault));
    }

    private void SetBordersActive(bool isActive)
    {
        foreach (GameObject border in borders)
        {
            if (border != null)
                border.SetActive(isActive);
        }
    }

    IEnumerator ZoomCamera(float targetSize)
    {
        if (mainCamera == null) yield break;

        float startSize = mainCamera.orthographicSize;
        float t = 0f;

        while (Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.05f)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }

        mainCamera.orthographicSize = targetSize;
    }

    IEnumerator FadeIn(GameObject obj, float duration)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color endColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            sr.color = Color.Lerp(new Color(sr.color.r, sr.color.g, sr.color.b, 0f), endColor, t / duration);
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
        float t = 0f;

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
