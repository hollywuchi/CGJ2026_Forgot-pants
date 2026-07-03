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

    public static event Action<ParticalEffectType, Vector3> ParticalEffectEvent;
    public static void CallParticalEffectEvent(ParticalEffectType type, Vector3 pos)
    {
        ParticalEffectEvent?.Invoke(type, pos);
    }

    public static event Action<SoundDetails> InitSoundEffect;
    public static void CallInitSoundEffect(SoundDetails sound)
    {
        InitSoundEffect?.Invoke(sound);
    }

}
