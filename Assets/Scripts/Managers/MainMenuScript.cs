using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject mainMenu, levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        //Ensure that the timescale always starts at 1
        Time.timeScale = 1;
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        Cursor.visible = true;
    }

    public void ToChooseLevel()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneNum) //0 for main menu, everything else is as it is numbered
    {
        SceneManager.LoadScene(sceneBuildIndex: sceneNum);
    }
}
