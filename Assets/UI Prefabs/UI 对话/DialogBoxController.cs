using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogBoxController : MonoBehaviour
{
    [Header("组件引用")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI speakerLabel;
    [SerializeField] private TextMeshProUGUI dialogContent;
    [SerializeField] private TextMeshProUGUI continueLabel;

    private bool canContinue = false;

    private void Start()
    {
        backgroundImage.gameObject.SetActive(true);
        continueLabel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 外部调用：显示一句对话。
    /// 调用方在协程中逐句调用此方法，传入角色名和文本。
    /// </summary>
    public void ShowLine(string speakerName, string dialogText)
    {
        speakerLabel.text = speakerName;
        dialogContent.text = dialogText;
        canContinue = false;
        continueLabel.gameObject.SetActive(true);
    }

    /// <summary>
    /// 玩家点击“继续”按钮时调用此方法。
    /// 调用方在协程中等待此标志后执行下一句或关闭。
    /// </summary>
    public void OnContinueClicked()
    {
        canContinue = true;
    }

    /// <summary>
    /// 外部调用：等待玩家点击继续按钮。
    /// 调用方在协程中 yield return 此方法。
    /// </summary>
    public System.Collections.IEnumerator WaitForContinue()
    {
        while (!canContinue)
            yield return null;
        canContinue = false;
    }

    /// <summary>
    /// 关闭对话框。
    /// </summary>
    public void Close()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        // 如果背景图没有挂载 Button，自动添加并绑定点击事件
        if (backgroundImage != null)
        {
            Button bgButton = backgroundImage.GetComponent<Button>();
            if (bgButton == null)
                bgButton = backgroundImage.gameObject.AddComponent<Button>();
            bgButton.onClick.RemoveAllListeners();
            bgButton.onClick.AddListener(OnContinueClicked);
        }
    }

}