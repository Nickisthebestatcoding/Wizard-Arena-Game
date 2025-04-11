using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    string[] levelNames = {"TutorialWorld", "Level1"};
    int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("TutorialWorld", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Changelevel()
    {
        string oldscene = levelNames[currentLevel];

        currentLevel++;
        if (currentLevel >= levelNames.Length)
        {
            currentLevel = 0;
        }

        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);

        SceneManager.UnloadSceneAsync("TutorialWorld");
    }
}
