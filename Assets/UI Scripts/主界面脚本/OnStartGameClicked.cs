using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartGameClicked : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject loderPanel;
    public void StartButton()
    {
        mainPanel.SetActive(false);
        loderPanel.SetActive(true);
    }
}
