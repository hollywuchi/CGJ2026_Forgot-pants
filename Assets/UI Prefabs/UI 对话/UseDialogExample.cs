using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDialogExample : MonoBehaviour
{
    public GameObject dialogPrefab;

    public void OnStartDialog()
    {
        StartCoroutine(DialogCoroutine());
    }

    private IEnumerator DialogCoroutine()
    {
        // 实例化对话框预制体
        GameObject obj = UIManager.Instance.ShowPanelReturn(dialogPrefab);
        DialogBoxController dialog = obj.GetComponent<DialogBoxController>();

        // 逐句显示对话
        yield return StartCoroutine(ShowLineAndWait(dialog, "角色A", "你好，冒险者。"));
        yield return StartCoroutine(ShowLineAndWait(dialog, "角色B", "你是谁？"));
        yield return StartCoroutine(ShowLineAndWait(dialog, "角色A", "我是这里的向导。"));
        yield return StartCoroutine(ShowLineAndWait(dialog, "角色B", "原来如此,谢谢。"));
        yield return StartCoroutine(ShowLineAndWait(dialog, "角色A", "前面危险,请小心。"));

        //全部对话结束，关闭对话框
        dialog.Close();
    }

    private IEnumerator ShowLineAndWait(DialogBoxController dialog, string speaker, string text)
    {
        dialog.ShowLine(speaker, text);
        yield return dialog.WaitForContinue();
    }
}