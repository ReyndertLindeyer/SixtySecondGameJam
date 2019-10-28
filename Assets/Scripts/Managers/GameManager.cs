using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private float timeLeft;

    private GameObject player;
    public Text lifeText = null;
    public Text timeText = null;

    private int playerHealth;

    private bool gameIsPaused;

    private UIManager uiManager;
    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        //Ensure that the timescale always starts at 1
        Time.timeScale = 1;

        //Ensure that cursor starts hiden during gameplay
        Cursor.visible = false;

        timeLeft = 60.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        gameIsPaused = false;

        uiManager = GameObject.FindObjectOfType<Canvas>().GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerMainScript>().GetLivesRemaining();

        if (timeLeft <= 0.0f)
        {
            //Pause game and display win screen
            Time.timeScale = 0;
            Cursor.visible = true;
            uiManager.SetWinMenuActive(true);
        }

        if(playerHealth <= 0)
        {
            //Pause game and display lose screen
            Time.timeScale = 0;
            Cursor.visible = true;
            uiManager.SetLoseMenuActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGamePaused();
        }

        lifeText.text = "" + playerHealth;

        timeLeft -= Time.deltaTime;

        timeText.text = "" + System.Math.Round(timeLeft, 2);
    }

    public void SetGamePaused()
    {
        if ((timeLeft > 0.0f) && (playerHealth > 0))
        {
            if (!gameIsPaused)
            {
                Time.timeScale = 0;
                uiManager.SetPauseMenuActive(true);
                Cursor.visible = true;
                gameIsPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                uiManager.SetPauseMenuActive(false);
                Cursor.visible = false;
                gameIsPaused = false;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    public void ToLevel(int levelNum)
    {
        SceneManager.LoadScene(sceneBuildIndex: levelNum);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        uiManager.SetWinMenuActive(false);
        uiManager.SetLoseMenuActive(false);
        uiManager.SetPauseMenuActive(false);

        Cursor.visible = false;

        timeLeft = 60.0f;
        gameIsPaused = false;

        enemyManager.ResetAllEnemies();

        player.GetComponent<PlayerMainScript>().ResetObject();
    }
}
