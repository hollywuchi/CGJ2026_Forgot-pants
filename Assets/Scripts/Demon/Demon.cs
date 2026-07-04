using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Demon : MonoBehaviour
{
    [Header("心魔属性")]
    [Tooltip("心魔的攻击范围")]
    public float AttackRange;
    [Tooltip("心魔的移动速度")]
    public float moveSpeed;
    [Tooltip("心魔在光亮中的消失时间")]
    public float fadeTime;
    [Tooltip("心魔在黑暗中出现的时间")]
    public float appearTime;
    [Header("引用组件")]
    public Transform player;
    [HideInInspector] public bool isInLight;
    [HideInInspector] public bool needFading;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private CircleCollider2D attackColl;
    private SpriteRenderer demonSpriteRenderer;
    private Animator demonAnimator;
    private bool isPased;
    private bool isGaming;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        attackColl = GetComponent<CircleCollider2D>();
        demonSpriteRenderer = GetComponent<SpriteRenderer>();
        demonAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (needFading)
        {
            demonSpriteRenderer.DOFade(0, fadeTime)
            .SetEase(Ease.OutSine)
            .OnComplete(() => attackColl.excludeLayers = LayerMask.GetMask("Player"));
        }
        else
        {
            demonSpriteRenderer.DOFade(1, appearTime)
            .SetEase(Ease.OutSine)
            .OnComplete(() => attackColl.excludeLayers = 0);
        }
    }

    void OnEnable()
    {
        EventHandler.UpdateGameStateEvent += OnUpdateGameStateEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    void OnDisable()
    {
        EventHandler.UpdateGameStateEvent -= OnUpdateGameStateEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        demonSpriteRenderer.DOKill();
    }


    private void FixedUpdate()
    {
        if (!needFading && !isInLight && !isPased && isGaming)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnUpdateGameStateEvent(GameState state)
    {
        switch (state)
        {
            case GameState.Pause:
                isPased = true;
                break;
            case GameState.GamePlay:
                isPased = false;
                break;
        }
    }

    private void OnStartNewGameEvent(int obj)
    {
        isGaming = true;
    }

}
