using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloseCurrentPanel : MonoBehaviour
{
    public void OnCloseClick()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        CanvasGroup cg = canvas.GetComponent<CanvasGroup>();
        if (cg == null) cg = canvas.gameObject.AddComponent<CanvasGroup>();
        Sequence seq = DOTween.Sequence();
        seq.Join(canvas.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack));
        seq.Join(cg.DOFade(0f, 0.2f));
        seq.OnComplete(() =>
        {
            canvas.transform.localScale = Vector3.one;
            cg.alpha = 1f;
            canvas.gameObject.SetActive(false);
        });
    }
}