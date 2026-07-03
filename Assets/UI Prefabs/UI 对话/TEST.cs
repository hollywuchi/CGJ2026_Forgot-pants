using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UseDialogExample;
public class TEST : MonoBehaviour
{
    public void OnTestDialog()
    {


        //此脚本不直接调用，把下面三段写入程序脚本


        //在程序的脚本直接写在start里
        GameObject manager = GameObject.Find("$Manager");
        Transform dialogManagerTransform = manager.transform.Find("DialogManager");
        UseDialogExample dialog = dialogManagerTransform.GetComponent<UseDialogExample>();

        //存储对话内容
        List<DialogLine> lines = new List<DialogLine>
        {
            new DialogLine { speakerName = "111", dialogText = "222" ,portraitName = "001"},
            new DialogLine { speakerName = "234", dialogText = "567",portraitName = "001" },
            new DialogLine { speakerName = "890", dialogText = "00012",portraitName = "000" }
        };
        //最终方法调用
        dialog.StartDialog(lines);
    }
}