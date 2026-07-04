using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Anchor : MonoBehaviour
{
    [Tooltip("锚点的半径")]
    public float radius = 5f;
    public Sprite spriteActive;
    public Transform playerSavePoint;
    private CircleCollider2D coll;
    private Light2D light2D;
    private DialogueControler dialogue;
    public bool isActive;

    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        light2D = GetComponent<Light2D>();
        dialogue = GetComponent<DialogueControler>();
        if (light2D == null) light2D.enabled = false;
    }
    void OnEnable()
    {
        isActive = false;
    }

    void Update()
    {
        coll.radius = radius;
    }

    public void DialogueFinish()
    {
        isActive = true;
        if (light2D != null) light2D.enabled = true;
        if (spriteActive != null)
        {
            GetComponent<SpriteRenderer>().sprite = spriteActive;
        }
        EventHandler.CallPlayerSavePointEvent(playerSavePoint.position);
    }


}
