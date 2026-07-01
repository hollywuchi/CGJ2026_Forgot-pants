using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UISceneManager : MonoBehaviour
{
    [Header("拖入你的主菜单和设置面板")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    void Awake()
    {
        // 获取当前正在运行的 UI 场景
        Scene currentScene = SceneManager.GetSceneByName("UI"); 

        
        if (currentScene.isLoaded && SceneManager.sceneCount > 1)
        {
            // 直接显示设置，隐藏主菜单
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (settingsPanel != null) settingsPanel.SetActive(true);
        }
        else
        {
            // 正常启动游戏，显示主菜单，隐藏设置
            if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
            if (settingsPanel != null) settingsPanel.SetActive(false);
        }
    }
}
