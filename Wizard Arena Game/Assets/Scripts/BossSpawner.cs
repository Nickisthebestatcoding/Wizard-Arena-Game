using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public Transform cameraFocusTarget; // ?? Drag necromancer or spawn point here

    public GameObject[] borders;
    private Health bossHealth;

    public Camera mainCamera;
    public float zoomDuringSummon = 4f;
    public float zoomDuringFight = 6f;
    public float zoomDefault = 8f;
    public float zoomSpeed = 2f;

    private bool hasSpawned = false;
    private Vector3 originalCamPos;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        SetBordersActive(false);
        originalCamPos = mainCamera.transform.position;
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
        // 1. Save original camera position
        originalCamPos = mainCamera.transform.position;

        // 2. Move camera to focus target
        if (cameraFocusTarget != null)
        {
            Vector3 focusPos = new Vector3(cameraFocusTarget.position.x, cameraFocusTarget.position.y, originalCamPos.z);
            mainCamera.transform.position = focusPos;
        }

        // 3. Zoom in on target
        yield return StartCoroutine(ZoomCamera(zoomDuringSummon));
        yield return new WaitForSeconds(1f);

        // 4. Spawn boss
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
            bossHealth = boss.GetComponent<Health>();
        }

        // 5. Zoom out to default and return to original camera position
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(ZoomCamera(zoomDefault));
        mainCamera.transform.position = originalCamPos;

        // 6. Zoom in slightly and stay that way for fight
        yield return new WaitForSeconds(0.5f);
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
