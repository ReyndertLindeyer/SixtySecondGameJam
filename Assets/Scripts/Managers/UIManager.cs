using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

    public void SetPauseMenuActive(bool active)
    {
        pauseMenu.SetActive(active);
    }

    public void SetWinMenuActive(bool active)
    {
        winMenu.SetActive(active);
    }

    public void SetLoseMenuActive(bool active)
    {
        loseMenu.SetActive(active);
    }
}
