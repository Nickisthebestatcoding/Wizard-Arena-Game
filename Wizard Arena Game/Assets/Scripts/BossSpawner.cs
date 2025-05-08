using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public GameObject gateToActivate;
    public Camera mainCamera;
    public Transform player;
    public float zoomInSize = 3f;
    public float zoomDuration = 1f;
    public float holdTime = 1.5f;

    private bool bossSpawned = false;
    private float originalSize;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        originalSize = mainCamera.orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bossSpawned) return;

        if (other.CompareTag("Wizard"))
        {
            bossSpawned = true;
            StartCoroutine(SpawnBossSequence());
        }
    }

    IEnumerator SpawnBossSequence()
    {
        gameObject.SetActive(false);

        if (gateToActivate != null)
            gateToActivate.SetActive(true);

        // Zoom in on boss spawn point
        yield return StartCoroutine(ZoomCamera(bossSpawnPoint.position, zoomInSize));

        // Spawn boss and fade in
        GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        SpriteRenderer sr = boss.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            float fadeDuration = 1f;
            float t = 0;
            while (t < fadeDuration)
            {
                float alpha = t / fadeDuration;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                t += Time.deltaTime;
                yield return null;
            }
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }

        // Hold camera on boss
        yield return new WaitForSeconds(holdTime);

        // Zoom out to player
        yield return StartCoroutine(ZoomCamera(player.position, originalSize));
    }

    IEnumerator ZoomCamera(Vector3 targetPos, float targetSize)
    {
        Vector3 startPos = mainCamera.transform.position;
        float startSize = mainCamera.orthographicSize;

        float t = 0f;
        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            float progress = t / zoomDuration;

            // Lerp position (keep camera z position)
            Vector3 newPos = Vector3.Lerp(startPos, new Vector3(targetPos.x, targetPos.y, startPos.z), progress);
            mainCamera.transform.position = newPos;

            // Lerp zoom
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, progress);

            yield return null;
        }
    }
}
