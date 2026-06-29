using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    private GameObject PopupPanel;
    private Image im;

    //µ±Ç°µ¯´°
    private Vector3 originalScale;
    private Color originalColor;


    private void Start()
    {
        PopupPanel = transform.parent.gameObject;
        im = GetComponent<Image>();

        //´æ´¢µ±Ç°µ¯´°
        originalScale = transform.localScale;
        originalColor = im.color;


    }
    public void OnConfirmButton()
    {
        PopupPanel.transform.DOScale(0, 0.15f);
        im.DOFade(0, 0.15f).OnComplete(()=>
        {
            //µ¯´°ÏûÊ§Ç°¸´Ô­µ¯´°
            PopupPanel.transform.localScale = originalScale;
            im.color = originalColor;

            PopupPanel.SetActive(false);
        }
        );
        
    }
}
