using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这里放置事件处理器的代码
/// </summary>
public static class EventHandler
{
    /// <summary>
    /// 触发传送事件
    /// </summary>
    public static event Action<string, Vector3> TransitionEvent;
    public static void CallTransitionEvent(string sceneName, Vector3 position)
    {
        TransitionEvent?.Invoke(sceneName, position);
    }

    /// <summary>
    /// 场景转换前执行的事件
    /// </summary>
    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    /// <summary>
    /// 场景转换后执行的事件
    /// </summary>
    public static event Action AfterSceneLoadEvent;
    public static void CallAfterSceneLoadEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }

    /// <summary>
    /// 新游戏事件
    /// </summary>
    public static event Action<int> StartNewGameEvent;
    public static void CallStartNewGameEvent(int obj)
    {
        StartNewGameEvent?.Invoke(obj);
    }

    public static event Action<SoundName> PlaySoundEvent;
    public static void CallPlaySoundEvent(SoundName soundName)
    {
        PlaySoundEvent?.Invoke(soundName);
    }

    public static event Action<SoundDetails> InitSoundEffect;
    public static void CallInitSoundEffect(SoundDetails sound)
    {
        InitSoundEffect?.Invoke(sound);
    }

    public static event Action PlayerHurtEvent;
    public static void CallPlayerHurtEvent()
    {
        PlayerHurtEvent?.Invoke();
    }

    public static event Action PlayerDieEvent;
    public static void CallPlayerDieEvent()
    {
        PlayerDieEvent?.Invoke();
    }

    public static event Action<DialoguePiece> ShowDialogueEvent;
    public static void CallShowDialogueEvent(DialoguePiece piece)
    {
        ShowDialogueEvent?.Invoke(piece);
    }

    public static event Action<Vector3> PlayerSavePointEvent;
    public static void CallPlayerSavePointEvent(Vector3 pos)
    {
        PlayerSavePointEvent?.Invoke(pos);
    }

    public static event Action PlayerRebornEvent;
    public static void CallPlayerRebornEvent()
    {
        PlayerRebornEvent?.Invoke();
    }

    public static event Action<GameState> UpdateGameStateEvent;
    public static void CallUpdateGameStateEvent(GameState state)
    {
        UpdateGameStateEvent?.Invoke(state);
    }

    public static event Action EndGameEvent;
    public static void CallEndGameEvent()
    {
        EndGameEvent?.Invoke();
    }
}
