using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public GameObject mainPanel;
    public GameObject rePanel;
    void OnEnable()
    {
        EventHandler.PlayerDieEvent += () => rePanel.SetActive(true);
    }

    void OnDisable()
    {
        EventHandler.PlayerDieEvent -= () => rePanel.SetActive(true);
    }


    public void StartGame()
    {
        EventHandler.CallStartNewGameEvent(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        EventHandler.CallRestartGameEvent();
    }
}
