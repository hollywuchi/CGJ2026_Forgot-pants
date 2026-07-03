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

    [Header("角色立绘容器")]
    [SerializeField] private Image characterPortrait;

    private bool canContinue = false;
    private Button textButton;

    private void Start()
    {
        backgroundImage.gameObject.SetActive(true);
        continueLabel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (dialogContent != null)
        {
            textButton = dialogContent.GetComponent<Button>();
            if (textButton == null)
                textButton = dialogContent.gameObject.AddComponent<Button>();
            textButton.onClick.RemoveAllListeners();
            textButton.onClick.AddListener(OnContinueClicked);
        }
    }

    public void ShowLine(string speakerName, string dialogText)
    {
        speakerLabel.text = speakerName;
        dialogContent.text = dialogText;
        canContinue = false;
        continueLabel.gameObject.SetActive(true);

        // ===== 新增：根据说话人名称自动加载立绘 =====
        if (characterPortrait != null)
        {
            // 假设立绘图片放在 Assets/Resources/Portraits/ 下，名称与 speakerName 一致
            Sprite portrait = Resources.Load<Sprite>($"Portraits/{speakerName}");
            if (portrait != null)
            {
                characterPortrait.sprite = portrait;
                characterPortrait.gameObject.SetActive(true);
            }
            else
            {
                // 如果找不到对应图片，隐藏立绘容器
                characterPortrait.gameObject.SetActive(false);
            }
        }
    }

    public void OnContinueClicked()
    {
        canContinue = true;
    }

    public IEnumerator WaitForContinue()
    {
        yield return null;
        while (!canContinue)
            yield return null;
        canContinue = false;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
    public void ShowLine(DialogLine line)
    {
        ShowLine(line.speakerName, line.dialogText);

        if (!string.IsNullOrEmpty(line.portraitName) && characterPortrait != null)
        {
            Sprite portrait = Resources.Load<Sprite>($"Portraits/{line.portraitName}");
            if (portrait != null)
            {
                characterPortrait.sprite = portrait;
                characterPortrait.gameObject.SetActive(true);
            }
            else
            {
                characterPortrait.gameObject.SetActive(false);
            }
        }
    }


}
