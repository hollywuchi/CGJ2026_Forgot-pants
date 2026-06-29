using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject mainPanel;

    public void returnButton()
    {
        settingPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
