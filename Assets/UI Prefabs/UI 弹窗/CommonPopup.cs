using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 通用弹窗脚本，挂在弹窗预制体的根节点上。
/// 提供入场/出场动画，点击确定按钮后自动销毁并清理场景中的残留实例。
/// 外部调用方式：popup.Show("要显示的文本");
/// </summary>
public class CommonPopup : MonoBehaviour
{
    [Header("组件引用（拖拽）")]
    [SerializeField] private TextMeshProUGUI contentText;  // 显示文本的组件
    [SerializeField] private Button confirmButton;           // 确定按钮

    [Header("动画参数（直接改数字）")]
    [SerializeField] private float appearDuration = 0.2f;   // 入场动画时长
    [SerializeField] private float disappearDuration = 0.15f; // 出场动画时长
    [SerializeField] private float showScale = 1f;          // 弹窗原始大小
    [SerializeField] private float hideScale = 0f;          // 弹窗缩到多小

    private CanvasGroup canvasGroup;
    private string prefabName; // 用于清理场景中同名的残留弹窗

    private void Awake()
    {
        // 初始化状态
        gameObject.SetActive(false);
        prefabName = gameObject.name.Replace("(Clone)", "").Trim();

        // 绑定确定按钮事件
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }

    /// <summary>
    /// 外部调用接口：显示弹窗并设置文本
    /// </summary>
    public void Show(string message)
    {
        contentText.text = message;
        gameObject.SetActive(true);
        PlayAppearAnimation();
    }

    /// <summary>
    /// 入场动画：从小到大 + 淡入
    /// </summary>
    private void PlayAppearAnimation()
    {
        // 获取或添加 CanvasGroup
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // 初始状态
        canvasGroup.alpha = 0f;
        transform.localScale = Vector3.one * hideScale;

        // 播放动画
        Sequence appearSequence = DOTween.Sequence();
        appearSequence.Join(transform.DOScale(Vector3.one * showScale, appearDuration).SetEase(Ease.OutBack));
        appearSequence.Join(canvasGroup.DOFade(1f, appearDuration));
    }

    /// <summary>
    /// 点击确定按钮时触发
    /// </summary>
    private void OnConfirmClicked()
    {
        // 停止所有动画，防止残留
        transform.DOKill();
        if (canvasGroup != null)
            canvasGroup.DOKill();

        // 播放出场动画：缩小 + 淡出
        Sequence disappearSequence = DOTween.Sequence();
        disappearSequence.Join(transform.DOScale(Vector3.one * hideScale, disappearDuration).SetEase(Ease.InBack));
        disappearSequence.Join(canvasGroup.DOFade(0f, disappearDuration));
        disappearSequence.OnComplete(OnCloseComplete);
    }

    /// <summary>
    /// 动画结束后的回调：销毁自身 + 清理场景中的残留弹窗
    /// </summary>
    private void OnCloseComplete()
    {
        // 1. 清理场景中所有由同一预制体生成的、尚未销毁的弹窗实例
        CleanupRemainingPopups();

        // 2. 销毁当前弹窗
        Destroy(gameObject);
    }

    /// <summary>
    /// 查找并销毁场景中所有同名的弹窗实例（防止 Hierarchy 堆积）
    /// </summary>
    private void CleanupRemainingPopups()
    {
        // 找到场景中所有类型为 CommonPopup 的弹窗
        CommonPopup[] allPopups = FindObjectsByType<CommonPopup>(FindObjectsSortMode.None);
        foreach (CommonPopup popup in allPopups)
        {
            // 跳过当前这个弹窗（稍后单独销毁）
            if (popup.gameObject == this.gameObject)
                continue;

            // 如果弹窗的预制体名称匹配（说明是同一个预制体生成的），立即销毁
            string otherName = popup.gameObject.name.Replace("(Clone)", "").Trim();
            if (otherName == prefabName)
            {
                // 停止它的动画再销毁，防止报错
                popup.transform.DOKill();
                if (popup.canvasGroup != null)
                    popup.canvasGroup.DOKill();
                Destroy(popup.gameObject);
            }
        }
    }
}