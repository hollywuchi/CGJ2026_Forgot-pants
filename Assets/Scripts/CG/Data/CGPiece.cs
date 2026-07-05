using UnityEngine;

[System.Serializable]
public class CGPiece
{
    [Header("CG详情")]
    [TextArea]
    public string dialogueText;
    public float displayDuration = 3f;
    public bool waitForInput = false;
    [HideInInspector] public bool isDown;
}
