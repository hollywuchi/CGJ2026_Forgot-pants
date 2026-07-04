using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dialogueList
{
    public List<DialoguePiece> dialogues;
}

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/NPCDialogueList_SO")]
public class NPCDialogueList_SO : ScriptableObject
{
    public dialogueList npcDialogueList;

    // 修改返回类型为 List<DialoguePiece>
    public List<DialoguePiece> InitDialogueDic()
    {
        // 检查非空并直接返回列表内的对话数据
        if (npcDialogueList != null && npcDialogueList.dialogues != null)
        {
            return npcDialogueList.dialogues;
        }
        
        // 如果没有数据，则返回一个空列表以防报错
        return new List<DialoguePiece>();
    }
}