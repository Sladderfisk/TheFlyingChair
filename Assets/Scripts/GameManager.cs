using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private const int START_LVL = 1;
    private const int MENU_SCENE = 0;
    public int CurrentLvl { get; private set; }

    private void Awake()
    {
        Init();
        CurrentLvl = SceneManager.GetActiveScene().buildIndex;
    }

    private void Init()
    {
        var totalManagers = FindObjectsOfType<GameManager>();

        if (totalManagers.Length > 1)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void Die(float waitTime)
    {
        // Something should happen
        // Probably restart the current scene.

        StartCoroutine(Dying(waitTime));
    }

    private IEnumerator Dying(float time)
    {
        yield return new WaitForSeconds(time);
        RestartScene();
    }

    public void RestartScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadFirstLvl()
    {
        LoadScene(START_LVL);
    }

    public void LoadMenu()
    {
        LoadScene(MENU_SCENE);
    }

    public void LoadNextLvl()
    {
        LoadScene(CurrentLvl + 1);
    }
}
