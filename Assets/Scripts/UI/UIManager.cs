using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public GameObject mainPanel;
    void OnEnable()
    {
    }

    void OnDisable()
    {
    }


    public void StartGame()
    {
        EventHandler.CallStartNewGameEvent(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
