using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndGameUI : MonoBehaviour
{



    public void OnShowCredits()
    {
        // 隐藏当前结束面板
        gameObject.SetActive(false);

        // 显示 End Canvas
        UIManager.Instance.ShowChildPanel(" EndCanvas");
    }
}
