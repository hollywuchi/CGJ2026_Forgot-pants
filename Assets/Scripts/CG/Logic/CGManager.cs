using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CGManager : Singleton<CGManager>
{
    [Header("UI组件")]
    public CGUI cgUI;

    [Header("所有CG数据")]
    public CGDialogueList_SO[] allCGData;

    [Header("对应CG图片列表")]
    public Sprite[] cgImageList;

    [Header("CG完成事件")]
    public UnityEvent OnCGFinishEvent;

    private CGController currentController;
    private bool isPlaying;

    protected override void Awake()
    {
        base.Awake();
        currentController = gameObject.AddComponent<CGController>();
    }

    void OnEnable()
    {
        EventHandler.PlayCGEvent += OnPlayCGEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    void OnDisable()
    {
        EventHandler.PlayCGEvent -= OnPlayCGEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }


    private void OnPlayCGEvent(int index)
    {
        if (!isPlaying && allCGData.Length > 0)
        {
            PlayCG(index);
        }
    }

    public void PlayCG(int index)
    {
        if (index >= 0 && index < allCGData.Length)
        {
            currentController.cgDataList = allCGData[index];
            
            // 使用对应索引的图片，如果没有则使用默认图片
            if (cgImageList != null && index < cgImageList.Length && cgImageList[index] != null)
            {
                currentController.defaultCGImage = cgImageList[index];
            }
            
            currentController.OnCGFinishEvent = OnCGFinishEvent;
            currentController.PlayCG();
        }
        else
        {
            Debug.LogWarning($"CG索引越界: {index}，总共有{allCGData.Length}段CG");
        }
    }

    public void PlayCG(string cgName)
    {
        for (int i = 0; i < allCGData.Length; i++)
        {
            if (allCGData[i].name == cgName)
            {
                PlayCG(i);
                return;
            }
        }
    }

    public int GetCGCount()
    {
        return allCGData.Length;
    }

    private void OnStartNewGameEvent(int obj)
    {
        EventHandler.CallPlayCGEvent(0);
    }

    public void OnAfterCGFinishEvent()
    {
        EventHandler.CallAfterCGFinishEvent();
    }
}
