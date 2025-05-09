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

    public Transform zoomTarget; // Object to zoom into (can be the BossSpawner or any object)

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
        // Zoom in on the target (object or boss spawner)
        yield return StartCoroutine(ZoomCameraToObject(zoomTarget, zoomDuringSummon));
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

    // New coroutine to zoom to a specific object
    IEnumerator ZoomCameraToObject(Transform target, float targetSize)
    {
        if (mainCamera == null || target == null) yield break;

        Vector3 targetPosition = target.position;
        float startSize = mainCamera.orthographicSize;
        Vector3 startPosition = mainCamera.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            // Move the camera towards the target object
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            // Zoom the camera towards the target size
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }

        // Ensure we end exactly at the target position and size
        mainCamera.transform.position = targetPosition;
        mainCamera.orthographicSize = targetSize;
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
