using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject wizard;
    private Vector3 wizardStartPos;
    private Quaternion wizardStartRot;
    private Health wizardHealth;

    private List<GameObject> enemies = new List<GameObject>();
    private Dictionary<GameObject, Vector3> enemyStartPos = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> enemyStartRot = new Dictionary<GameObject, Quaternion>();




    // Start is called before the first frame update
    void Start()
    {
        wizard = GameObject.FindGameObjectWithTag("Wizard");
        if (wizard != null)
        {
            wizardStartPos = wizard.transform.position;
            wizardStartRot = wizard.transform.rotation;
            wizardHealth = wizard.GetComponent<Health>();
        }

        GameObject[] enemyObjs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjs)
        {
            enemies.Add(enemy);
            enemyStartPos[enemy] = enemy.transform.position;  // Storing position
            enemyStartRot[enemy] = enemy.transform.rotation;  // Storing rotation
        }
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void ResetLevel()
    {
        Debug.Log("🔁 Resetting Level 🔁");

        // Wizard Reset
        if (wizard != null)
        {
            wizard.transform.position = wizardStartPos;
            wizard.transform.rotation = wizardStartRot;
            wizard.SetActive(true);

            if (wizardHealth != null)
                wizardHealth.ResetHealth();

            // 🔧 Reset dash state
            Dash2D dashScript = wizard.GetComponent<Dash2D>();
            if (dashScript != null)
                dashScript.ResetDash();
        }

        // Enemies Reset
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.transform.position = enemyStartPos[enemy];
                enemy.transform.rotation = enemyStartRot[enemy];
                enemy.SetActive(true);

                Health hp = enemy.GetComponent<Health>();
                if (hp != null)
                    hp.ResetHealth();
            }
        }
    }

    public void ShowGameOver()
    {
        Debug.Log("💀 Game Over! 💀");

        // Optional: Trigger a UI Game Over screen here
        // For now, you can just pause the game or log
        // Time.timeScale = 0f;
    }
}

