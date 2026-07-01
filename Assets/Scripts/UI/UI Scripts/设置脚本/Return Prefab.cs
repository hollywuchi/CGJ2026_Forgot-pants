using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ReturnToGame : MonoBehaviour
{
    public void OnReturnClick()
    {

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            // 直接销毁整个 Canvas（包括所有子物体）
            Destroy(canvas.gameObject);
        }
        else
        {
            // 如果找不到 Canvas，直接销毁按钮所在的根对象
            Destroy(transform.root.gameObject);
        }
    }
}