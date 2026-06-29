using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这里放置事件处理器的代码
/// </summary>
public static class EventHandler
{
    public static event Action<string, Vector3> TransitionEvent;

    public static void TriggerTransitionEvent(string sceneName, Vector3 position)
    {
        TransitionEvent?.Invoke(sceneName, position);
    }
}
