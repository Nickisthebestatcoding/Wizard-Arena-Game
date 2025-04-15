using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Transform playerSpawnPoint;
    public GameObject player;
    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    private Health playerHealth;

    private List<knightbehavior> enemies = new List<knightbehavior >();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (player != null)
        {
            playerStartPos = playerSpawnPoint.position;
            playerStartRot = playerSpawnPoint.rotation;
            playerHealth = player.GetComponent<Health>();
        }

        // Store all enemy references
        enemies.AddRange(FindObjectsOfType<knightbehavior >()); // Make sure your enemies use a common Enemy script
    }
    public void ResetLevel()
    {
        // Reset player
        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRot;

        if (playerHealth != null)
        {
            playerHealth.ResetHealth(); // We’ll add this function next
        }

        // Reset enemies
        foreach (var enemy in enemies)
        {
            enemy.ResetEnemy(); // We’ll add this too
        }

        Debug.Log("Level reset. Timer unchanged.");
    }
}
