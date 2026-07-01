using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 在 UI 场景中实例化一个预制体，强制覆盖显示。
    /// </summary>
    /// <param name="panelPrefab">要实例化的 UI 预制体</param>
    public void ShowPanel(GameObject panelPrefab)
    {
        if (panelPrefab == null) return;


        // 在 UIManager 下实例化预制体
        GameObject newPanel = Instantiate(panelPrefab, transform);
        newPanel.name = panelPrefab.name; // 移除 "(Clone)" 后缀

        // 确保 GameObject 处于激活状态
        newPanel.SetActive(true);

        // 获取或添加 Canvas 组件，并强制设置为 ScreenSpaceOverlay
        Canvas canvas = newPanel.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = newPanel.AddComponent<Canvas>();
        }
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // 极高的 Sorting Order，确保不被遮挡

        // 确保有 CanvasScaler（可选，通常预制体自带）
        if (newPanel.GetComponent<CanvasScaler>() == null)
        {
            newPanel.AddComponent<CanvasScaler>();
        }

        // 确保有 GraphicRaycaster（可选，用于交互）
        if (newPanel.GetComponent<GraphicRaycaster>() == null)
        {
            newPanel.AddComponent<GraphicRaycaster>();
        }

    }


    //查找并打开子物体
    public void ShowChildPanel(string panelName)
    {
        Transform panel = transform.Find(panelName);
        panel.gameObject.SetActive(true);
    }




    //对话气泡调用
    public GameObject ShowPanelReturn(GameObject prefab)
    {
        GameObject newPanel = Instantiate(prefab, transform);
        newPanel.name = prefab.name;
        newPanel.SetActive(true);
        Canvas canvas = newPanel.GetComponent<Canvas>();
        if (canvas == null) canvas = newPanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        return newPanel;
    }

}