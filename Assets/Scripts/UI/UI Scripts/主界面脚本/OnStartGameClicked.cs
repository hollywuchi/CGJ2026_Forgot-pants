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
        //获取加载UI的LoadingScreen脚本
        loderPanel.GetComponent<LoadingScreen>().StartLoadingScene("PersistentScene");
    }
}