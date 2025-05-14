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

    [Header("Boss Spawner")]
    public BossSpawner bossSpawner;
    public Camera mainCamera;

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
            enemyStartPos[enemy] = enemy.transform.position;
            enemyStartRot[enemy] = enemy.transform.rotation;
        }
    }

    public void ResetLevel()
    {
        Debug.Log("🔁 Resetting Level 🔁");

        if (wizard != null)
        {
            wizard.transform.position = wizardStartPos;
            wizard.transform.rotation = wizardStartRot;
            wizard.SetActive(true);

            if (wizardHealth != null)
                wizardHealth.ResetHealth();

            Dash2D dashScript = wizard.GetComponent<Dash2D>();
            if (dashScript != null)
                dashScript.ResetDash();

            PoisonEffect poison = wizard.GetComponent<PoisonEffect>();
            if (poison != null)
                poison.ResetEffect();

            PlayerFreeze freeze = wizard.GetComponent<PlayerFreeze>();
            if (freeze != null)
                freeze.ResetEffect();
        }

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

        

        ResetCamera();
    }

    private void ResetCamera()
    {
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
            mainCamera.orthographicSize = 8f;
        }
    }

    public void ShowGameOver()
    {
        Debug.Log("💀 Game Over! 💀");
    }
}
