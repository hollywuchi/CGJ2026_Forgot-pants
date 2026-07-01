using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenBackpack : MonoBehaviour
{
    public void OnOpenBackpackClick()
    {
        UIManager.Instance.ShowChildPanel("backpack canvas");
    }
}