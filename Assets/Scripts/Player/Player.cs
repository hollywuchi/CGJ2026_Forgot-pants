using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;

    public GameObject lightPoint;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        moveInput = InputManager.Instance.moveInput;
    }

    void FixedUpdate()
    {
        Move();
        Move_Play();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        ChangeLigthDir();
    }

    private void Move_Play()
    {
        if (Mathf.Abs(moveInput.x) > 0.1f || Mathf.Abs(moveInput.y) > 0.1f)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("InputX", moveInput.x);
            anim.SetFloat("InputY", moveInput.y);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }


    private void ChangeLigthDir()
    {
        if (lightPoint != null && moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg + 90f;

            lightPoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
            lightPoint.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

}