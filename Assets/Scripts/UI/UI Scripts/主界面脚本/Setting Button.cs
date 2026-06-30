using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingPanel;
    public void settingButton()
    {
        mainPanel.SetActive(false);
        settingPanel.SetActive(true);
    }
}
