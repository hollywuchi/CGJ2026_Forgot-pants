using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public string mainMenuSceneName = "UI";           // 主菜单场景名称
    public string currentGameSceneName = "PersistentScene"; // 当前游戏场景名称

    public void OnReturnToMainMenu()
    {
        StartCoroutine(SwitchToMainMenu());
    }

    private System.Collections.IEnumerator SwitchToMainMenu()
    {
        // 0. 销毁当前场景中的 EventSystem，避免与 UI 场景的 EventSystem 冲突
        EventSystem currentEventSystem = FindObjectOfType<EventSystem>();
        if (currentEventSystem != null)
        {
            Destroy(currentEventSystem.gameObject);
        }
        // 1. 先异步加载主菜单场景（Additive 模式，不卸载当前场景）
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // 2. 主菜单场景已完全加载，现在可以安全销毁当前场景的残留 UI
        //    销毁设置面板（通过 UIManager 实例化的 Canvas）
        Canvas settingsCanvas = GetComponentInParent<Canvas>();
        if (settingsCanvas != null)
        {
            Destroy(settingsCanvas.gameObject);
        }
        //    销毁游戏场景中的关卡 UI（如果它没有随场景卸载而销毁）
        GameObject hudCanvas = GameObject.Find("关卡 UI Canvas"); // 改为你实际的名称
        if (hudCanvas != null)
        {
            Destroy(hudCanvas);
        }
        // 3. 重置 UIManager 到初始状态（只显示主菜单）
        UIManager.Instance.ResetToInitialState();
        // 4. 卸载当前游戏场景（此时主菜单场景已存在，不会触发警告）
        SceneManager.UnloadSceneAsync(currentGameSceneName);
    }
}