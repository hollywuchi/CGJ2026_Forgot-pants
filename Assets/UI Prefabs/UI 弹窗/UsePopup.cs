using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UsePopup : MonoBehaviour
{
    // 弹窗预制体拖拽
    public GameObject popupPrefab;

    public string popupText;

    // 这个方法可以绑定到按钮的 onClick 事件上
    public void OnClickOpenPopup()
    {

        if (popupPrefab == null) return;

        // 在场景中生成弹窗预制体
        GameObject popupObj = Instantiate(popupPrefab, Vector3.zero, Quaternion.identity);

        // 获取弹窗预制体上的脚本
        CommonPopup popup = popupObj.GetComponent<CommonPopup>();

        // 调用 Show 方法，传入你要显示的文本
        if (popup != null)
        {
            popup.Show(popupText);
        }
 
    }
}
