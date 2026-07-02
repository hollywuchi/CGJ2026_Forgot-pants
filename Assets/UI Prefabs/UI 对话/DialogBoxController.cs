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
    private Button bgButton;

    private void Start()
    {
        backgroundImage.gameObject.SetActive(true);
        continueLabel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (dialogContent != null)
        {
            Button textButton = dialogContent.GetComponent<Button>();
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
    }

    public void OnContinueClicked()
    {
        canContinue = true;
    }


      public IEnumerator WaitForContinue()
    {
        yield return null;

        // 循环等待，直到 canContinue 为 true
        while (!canContinue)
            yield return null;

        canContinue = false;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
