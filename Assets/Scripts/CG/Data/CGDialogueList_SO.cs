using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CG", menuName = "Dialogue/CGDialogueList_SO")]
public class CGDialogueList_SO : ScriptableObject
{
    public List<CGPiece> cgList = new List<CGPiece>();
}
