using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameConfirm : MonoBehaviour
{
 
    public GameObject teamButton;

    public void OnConfirmClicked()
    {
        // 1. 重置 UI 场景到初始状态（隐藏 End Canvas、BackPack Canvas 等，只留主界面面板）
        UIManager.Instance.ResetToInitialState();

        // 2. 确保主界面面板的“参与团队”按钮被激活
        if (teamButton != null)
        {
            teamButton.SetActive(true);
        }
    }
}