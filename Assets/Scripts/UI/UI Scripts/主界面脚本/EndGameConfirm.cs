using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndGameConfirm : MonoBehaviour
{
    public void OnConfirmClicked()
    {
        UIManager.Instance.ResetToInitialState();
    }
}