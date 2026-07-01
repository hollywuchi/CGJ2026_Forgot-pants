using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///调用预制体通用脚本


/// <summary>
/// 挂载在任何按钮上。在 Inspector 中将需要打开的面板预制体拖入 panelPrefab 字段。
/// 按钮点击时，将自动调用 UIManager 在 UI 场景中实例化并显示该预制体。
/// </summary>

public class OpenGameSettings: MonoBehaviour
{
    [Header("要打开的面板预制体")]
    public GameObject panelPrefab;

    public void OpenPanel()
    {
        UIManager.Instance.ShowPanel(panelPrefab);
    }
}