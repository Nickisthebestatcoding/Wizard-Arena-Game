using System.Collections;
using UnityEngine;

public class BossSummonTrigger : MonoBehaviour
{
    public GameObject skeletonBossPrefab;
    public Transform spawnPoint;

    public GameObject summoningCircle;
    public GameObject necromancer;

    public GameObject[] borders;
    private Health bossHealth;

    private bool hasSpawned = false;

    public float summonDelay = 2f;
    public float screenShakeDuration = 0.5f;
    public float screenShakeIntensity = 0.2f;

    [Header("Camera Zoom")]
    public Camera mainCamera;
    public float zoomDuringSummon = 4f;
    public float zoomDuringFight = 6f;
    public float zoomDefault = 8f;
    public float zoomSpeed = 2f;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

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

        // Zoom out more during summoning
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

        // Zoom in slightly for boss fight
        yield return StartCoroutine(ZoomCamera(zoomDuringFight));
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

    public void ResetBossState()
    {
        hasSpawned = false;

        // Reset camera zoom
        StartCoroutine(ZoomCamera(zoomDefault));

        if (bossHealth != null && bossHealth.gameObject != null)
        {
            Destroy(bossHealth.gameObject);
            bossHealth = null;
        }
    }
}
