using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private InputSystem inputActions;
    private Vector2 MoveInput;
    public Vector2 moveInput { get { return MoveInput; } }
    public bool dialogueInput;
    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputSystem();
    }

    void OnEnable()
    {
        inputActions.Enable();
        EventHandler.PlayerDieEvent += () => SetInputActive(false);
        EventHandler.UpdateGameStateEvent += OnUpdateGameStateEvent;
        EventHandler.PlayerRebornEvent += () => SetInputActive(true);
    }

    void OnDisable()
    {
        inputActions.Disable();
        EventHandler.PlayerDieEvent -= () => SetInputActive(false);
        EventHandler.UpdateGameStateEvent -= OnUpdateGameStateEvent;
        EventHandler.PlayerRebornEvent -= () => SetInputActive(true);
    }


    void Update()
    {
        MoveInput = inputActions.MoveSystem.Move.ReadValue<Vector2>();
        dialogueInput = inputActions.DialogueSystem.Dialogue.WasPressedThisFrame();
    }


    /// <summary>
    /// 更方便的控制输入系统的启用和禁用
    /// </summary>
    /// <param name="isActive"></param>
    public void SetInputActive(bool isActive)
    {
        if (isActive)
            inputActions.Enable();
        else
            inputActions.Disable();
    }

     private void OnUpdateGameStateEvent(GameState state)
    {
        switch (state)
        {
            case GameState.Pause:
                SetInputActive(false);
                break;
            case GameState.GamePlay:
                SetInputActive(true);
                break;
        }
    }

}