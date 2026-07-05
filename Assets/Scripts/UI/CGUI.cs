using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGUI : MonoBehaviour
{
    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        EventHandler.CallEndGameEvent();
    }
}
