using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGtrigger : MonoBehaviour
{
    public int cgIndex; // 用于指定要播放的CG索引
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventHandler.CallPlayCGEvent(cgIndex);
        }
    }
}
