using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DemonAttack : MonoBehaviour
{
    [Tooltip("心魔攻击的时间间隔")]
    public float attackDelay = 1f;
    private Coroutine attackCoroutine; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackCoroutine = StartCoroutine(ContinuousAttack());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    // 攻击协程
    private IEnumerator ContinuousAttack()
    {
        while (true) 
        {
            yield return new WaitForSeconds(attackDelay);

            EventHandler.CallPlayerHurtEvent();
        }
    }
}
