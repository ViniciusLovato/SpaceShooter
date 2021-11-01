using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private GameSession gameSession;


    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Scenes/Start Menu");
    }

    public void LoadStartGame()
    {
        SceneManager.LoadScene("Scenes/Level 1");
        gameSession.RestartScore();
    }

    public void LoadGameOver()
    {
        StartCoroutine(NextLevelAfterWait("Scenes/Game Over"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    private IEnumerator NextLevelAfterWait(string sceneName) {
        yield return new WaitForSeconds(1.0f);
    
        SceneManager.LoadScene(sceneName);
    }
}
