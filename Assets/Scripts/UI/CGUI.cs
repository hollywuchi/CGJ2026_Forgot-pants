using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CGUI : MonoBehaviour
{
    [Header("CG UI组件")]
    public GameObject cgPanel;
    public Image cgImage;
    public Text cgText;
    void OnEnable()
    {
        EventHandler.ShowCGEvent += OnShowCGEvent;
    }

    void OnDisable()
    {
        EventHandler.ShowCGEvent -= OnShowCGEvent;
    }

    private void OnShowCGEvent(CGPiece piece, Sprite image)
    {
        StartCoroutine(ShowCG(piece, image));
    }

    private IEnumerator ShowCG(CGPiece piece, Sprite image)
    {
        if (piece != null)
        {
            piece.isDown = false;
            cgPanel.SetActive(true);

            Sprite displayImage = image;

            if (displayImage != null)
            {
                cgImage.gameObject.SetActive(true);
                cgImage.sprite = displayImage;
            }
            else
            {
                cgImage.gameObject.SetActive(false);
            }

            if (!string.IsNullOrEmpty(piece.dialogueText))
            {
                cgText.gameObject.SetActive(true);
                cgText.text = string.Empty;
                yield return cgText.DOText(piece.dialogueText, 1f).WaitForCompletion();
            }
            else
            {
                cgText.gameObject.SetActive(false);
            }

            if (piece.waitForInput)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return));
            }

            piece.isDown = true;
        }
        else
        {
            cgPanel.SetActive(false);
            yield break;
        }
    }
}
