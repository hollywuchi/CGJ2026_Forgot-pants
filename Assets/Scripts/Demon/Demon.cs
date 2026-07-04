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
    private SpriteRenderer demonSpriteRenderer;
    private Animator demonAnimator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        demonSpriteRenderer = GetComponent<SpriteRenderer>();
        demonAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (needFading)
        {
            demonSpriteRenderer.DOFade(0, fadeTime)
            .SetEase(Ease.Linear)
            .OnComplete(() => coll.excludeLayers = LayerMask.GetMask("Player"));
        }
        else
        {
            demonSpriteRenderer.DOFade(1, appearTime)
            .SetEase(Ease.Linear)
            .OnComplete(() => coll.excludeLayers = 0);
        }
    }

    private void FixedUpdate()
    {
        if (!needFading || isInLight)
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

    void OnDisable()
    {
        demonSpriteRenderer.DOKill();
    }

}
