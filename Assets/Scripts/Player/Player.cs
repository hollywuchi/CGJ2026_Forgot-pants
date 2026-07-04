using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("玩家属性")]
    public float moveSpeed;
    [Tooltip("玩家的最大生命值")]
    public int maxHealth;
    [Tooltip("玩家无敌时间")]
    public float unbeatableTime;
    [Tooltip("玩家锚点数量")]
    public float anchorNum;
    [Tooltip("玩家锚点预制体")]
    public GameObject anchorPrefab;
    private int currentHealth;
    [HideInInspector] public bool isUnbeatable;

    [Header("引用组件")]
    [Tooltip("手电筒")]
    public GameObject flashLight;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;
    private Vector3 lastSavePosition;
    // private float lastSavePoint;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        EventHandler.PlayerHurtEvent += OnPlayerHurt;
        EventHandler.PlayerDieEvent += PlayerDie;
        EventHandler.PlayerSavePointEvent += OnPlayerSavePointEvent;
        EventHandler.PlayerRebornEvent += OnPlayerRebornEvent;
    }

    void OnDisable()
    {
        EventHandler.PlayerHurtEvent -= OnPlayerHurt;
        EventHandler.PlayerDieEvent -= PlayerDie;
        EventHandler.PlayerSavePointEvent -= OnPlayerSavePointEvent;
        EventHandler.PlayerRebornEvent -= OnPlayerRebornEvent;
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
        ChangeLightDir();
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

    /// <summary>
    /// 改变手电筒的方向
    /// </summary>
    private void ChangeLightDir()
    {
        if (flashLight != null && moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg + 90;

            flashLight.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnPlayerHurt()
    {
        if (isUnbeatable) return;
        if (currentHealth > 0)
        {
            print("玩家受伤");
            currentHealth -= 1;
            isUnbeatable = true;
            print("玩家无敌");
            DOVirtual.DelayedCall(unbeatableTime, () =>
            {
                isUnbeatable = false;
                print("玩家无敌结束");
            });
        }
        else if (currentHealth == 0)
            EventHandler.CallPlayerDieEvent();
    }

    private void PlayerDie()
    {
        // 执行死亡事件
        Debug.Log("玩家趋势了");
    }

    private void OnPlayerSavePointEvent(Vector3 vector)
    {
        lastSavePosition = vector;
    }

    private void OnPlayerRebornEvent()
    {
        transform.position = lastSavePosition;
        currentHealth = maxHealth;
        isUnbeatable = true;
        DOVirtual.DelayedCall(unbeatableTime, () =>
        {
            isUnbeatable = false;
            print("玩家无敌结束");
        });
    }

    private void PlayFootStepSound()
    {
        EventHandler.CallPlaySoundEvent(SoundName.FootSteps2);
    }

}