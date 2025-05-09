using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneLoader2D : MonoBehaviour
{
    public string sceneToLoad;

    private Collider2D objectCollider;

    void Start()
    {
        objectCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider == objectCollider)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
