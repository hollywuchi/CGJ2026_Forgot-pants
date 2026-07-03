using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogLine
{
    public string speakerName;
    public string dialogText;
    public string portraitName;   // 新增字段：立绘图片名称（如 "001"）
}

public class UseDialogExample : MonoBehaviour
{
    public GameObject dialogPrefab;

    private DialogBoxController cachedDialog;

    public void StartDialog(List<DialogLine> lines)
    {
        // 每次启动新对话时，重置缓存的引用，防止上次销毁后残留
        cachedDialog = null;
        StartCoroutine(DialogCoroutine(lines));
    }

    private IEnumerator DialogCoroutine(List<DialogLine> lines)
    {
        GameObject obj = UIManager.Instance.ShowPanelReturn(dialogPrefab);
        cachedDialog = obj.GetComponent<DialogBoxController>();

        foreach (DialogLine line in lines)
        {
            yield return StartCoroutine(ShowLineAndWait(line));  // 使用新重载
        }

        cachedDialog.Close();
    }

    private IEnumerator ShowLineAndWait(string speaker, string text)
    {
        cachedDialog.ShowLine(speaker, text);
        yield return cachedDialog.WaitForContinue();
    }

    private IEnumerator ShowLineAndWait(DialogLine line)
    {
        cachedDialog.ShowLine(line);  // 调用 DialogBoxController 的新方法
        yield return cachedDialog.WaitForContinue();
    }

}