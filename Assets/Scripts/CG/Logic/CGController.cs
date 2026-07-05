using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CGController : MonoBehaviour
{
    [Header("CG数据")]
    public CGDialogueList_SO cgDataList;
    [Header("默认CG图片（整个序列共用）")]
    public UnityEvent OnCGFinishEvent;

    public Sprite defaultCGImage; 
    private Stack<CGPiece> cgStack;
    private bool isPlaying;

    void OnEnable()
    {
        EventHandler.PlayCGEvent += OnPlayCGEvent;
    }

    void OnDisable()
    {
        EventHandler.PlayCGEvent -= OnPlayCGEvent;
    }

    private void OnPlayCGEvent(int index)
    {
        PlayCG();
    }

    public void PlayCG()
    {
        if (!isPlaying && cgDataList != null && cgDataList.cgList.Count > 0)
        {
            FillCGStack();
            StartCoroutine(CGPlaybackRoutine());
        }
    }

    private IEnumerator CGPlaybackRoutine()
    {
        isPlaying = true;
        EventHandler.CallUpdateGameStateEvent(GameState.Pause);

        while (cgStack.Count > 0)
        {
            CGPiece currentPiece = cgStack.Pop();
            currentPiece.isDown = false;
            EventHandler.CallShowCGEvent(currentPiece, defaultCGImage);

            if (currentPiece.waitForInput)
            {
                yield return new WaitUntil(() => currentPiece.isDown);
            }
            else
            {
                yield return new WaitForSeconds(currentPiece.displayDuration);
                currentPiece.isDown = true;
            }
        }

        EventHandler.CallShowCGEvent(null,null);
        EventHandler.CallUpdateGameStateEvent(GameState.GamePlay);
        EventHandler.CallEndCGEvent();

        if (OnCGFinishEvent != null)
        {
            OnCGFinishEvent.Invoke();
        }

        isPlaying = false;
    }

    private void FillCGStack()
    {
        cgStack = new Stack<CGPiece>();
        for (int i = cgDataList.cgList.Count - 1; i >= 0; i--)
        {
            cgDataList.cgList[i].isDown = false;
            cgStack.Push(cgDataList.cgList[i]);
        }
    }
}
