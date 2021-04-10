using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PauseMenu.GameIsPaused = false;
        GameOverMenu.GameIsOver = false;
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
