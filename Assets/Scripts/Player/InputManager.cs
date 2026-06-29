using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private InputSystem inputActions;
    private Vector2 MoveInput;
    public Vector2 moveInput { get { return MoveInput; } }
    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputSystem();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }


    void Update()
    {
        MoveInput = inputActions.MoveSystem.Move.ReadValue<Vector2>();
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

}