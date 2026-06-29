using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

/// 外部调用方式：popup.Show("要显示的文本");
/// 弹窗自带弹出、收缩动画，点击确定按钮后自动关闭。

public class CommonPopup : MonoBehaviour
{
    [Header("拖拽引用")]
    [SerializeField] private TextMeshProUGUI contentText;  // 显示文本内容的组件
    [SerializeField] private Button confirmButton;           // 确定按钮

    // 动画参数（硬编码在脚本里，想改直接改数字）
    private const float FADE_DURATION = 0.15f;     // 淡入淡出持续时间
    private const float SCALE_DURATION = 0.15f;    // 缩放动画持续时间
    private const float SHOW_SCALE = 1f;            //1=原始大小
    private const float HIDE_SCALE = 0f;            //0=缩到看不见

    private CanvasGroup canvasGroup;  // 用于控制透明度

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 外部调用此方法显示弹窗，传入要显示的文本
    /// </summary>
    public void Show(string message)
    {
        // 设置文本内容
        contentText.text = message;

        // 让弹窗显示出来
        gameObject.SetActive(true);

        // 播放出场动画
        PlayShowAnimation();

        // 绑定按钮的点击事件（每次显示时重新绑定，防止多次点击重复绑定）
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }


    /// 播放弹窗出场动画：从透明缩小状态，变为不透明原始大小

    private void PlayShowAnimation()
    {
        // 获取或添加CanvasGroup组件，用于控制透明度
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        // 开始时完全透明且大小为0
        canvasGroup.alpha = 0f;
        transform.localScale = Vector3.one * HIDE_SCALE;

        // 同时播放放大和淡入动画
        transform.DOScale(Vector3.one * SHOW_SCALE, SCALE_DURATION).SetEase(Ease.OutBack);
        canvasGroup.DOFade(1f, FADE_DURATION);
    }


    /// 点击确定按钮时触发：播放关闭动画，播放完毕后隐藏弹窗

    private void OnConfirmClicked()
    {
        // 先停止所有正在播放的动画，防止因为销毁导致动画报错
        transform.DOKill();
        if (canvasGroup != null)
            canvasGroup.DOKill();
        // 播放一个快速的缩小淡出动画，播完后直接销毁
        transform.DOScale(Vector3.one * HIDE_SCALE, SCALE_DURATION).SetEase(Ease.InBack);
        canvasGroup.DOFade(0f, FADE_DURATION).OnComplete(() =>
        {
            // 动画播放完毕，彻底销毁弹窗
            Destroy(gameObject);
        });
    }
}