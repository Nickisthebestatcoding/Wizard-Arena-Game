using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    public GameObject[] borders;
    private Health bossHealth;

    public Camera mainCamera;
    public float zoomDuringSummon = 4f;
    public float zoomDuringFight = 6f;
    public float zoomDefault = 8f;
    public float zoomSpeed = 2f;

    private bool hasSpawned = false;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

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
        // Zoom in
        yield return StartCoroutine(ZoomCamera(zoomDuringSummon));
        yield return new WaitForSeconds(1f);

        // Spawn boss
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
            bossHealth = boss.GetComponent<Health>();
        }

        // Zoom in slightly for boss fight
        yield return new WaitForSeconds(1f);
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

    public void ResetBossState()
    {
        hasSpawned = false;

        StartCoroutine(ZoomCamera(zoomDefault));

        if (bossHealth != null && bossHealth.gameObject != null)
        {
            Destroy(bossHealth.gameObject);
            bossHealth = null;
        }

        SetBordersActive(false);
        gameObject.SetActive(true); // Reactivate trigger
    }
}
