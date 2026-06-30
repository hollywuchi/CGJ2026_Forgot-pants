using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float Drag = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.drag = Drag;
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
        rb.AddRelativeForce(moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
}