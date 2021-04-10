using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameOverMenu : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject GameOverMenuUI;
    [SerializeField] Material backgoundMaterial;
    [SerializeField] Shader myShader;
    [SerializeField]
    private Player thePlayer;
    [SerializeField]
    private TextMeshProUGUI GameResult;

    public static bool isWin;
    void Start()
    {
        backgoundMaterial.shader = myShader;
    }
    // Update is called once per frame
    void Update()
    {
        if(thePlayer.GetComponent<Player>().IsPlayerDead && !GameIsOver)
        {
            GameResult.text = "GAME OVER";
            GameOver();
        }else if (isWin)
        {
            GameResult.text = "MISSION COMPLETE";
            isWin = false;
            GameOver();
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (GameIsOver)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}
    }

    public void ResumeWorld()
    {
        GameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
        thePlayer.reInvoke();
    }

    public void GameOver()
    {
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true;

    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeWorld();

    }
}
