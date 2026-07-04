using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerLight : MonoBehaviour
{
    public float timedely = 1.5f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Demon"))
        {
            DOVirtual.DelayedCall(timedely, () =>
            {
                other.GetComponent<Demon>().needFading = true;
            });
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Demon"))
        {
            DOVirtual.DelayedCall(timedely, () =>
            {
                other.GetComponent<Demon>().needFading = false;
            });
        }
    }
}
