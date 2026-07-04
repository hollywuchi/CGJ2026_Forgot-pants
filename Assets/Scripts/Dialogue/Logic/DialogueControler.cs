using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueControler : MonoBehaviour
{
    [Header("npc对话数据库")]
    public NPCDialogueList_SO dialogueDataList;
    public UnityEvent OnFinishEvent;
    public List<DialoguePiece> dialogueList = new List<DialoguePiece>();

    private Anchor anchor;
    private Stack<DialoguePiece> dialogueStack;
    private bool canTalk;
    private bool isActive;

    void Awake()
    {
        anchor = GetComponent<Anchor>();
    }

    void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
    }

    void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
    }

    private void OnAfterSceneLoadEvent()
    {
        if (dialogueDataList != null)
        {
            dialogueList = dialogueDataList.InitDialogueDic();
        }
        FillDialogueStake();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        canTalk = false;
    }

    void Update()
    {
        if (!isActive && canTalk && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DialogueRoutine());
        }
    }

    private IEnumerator DialogueRoutine()
    {
        if (dialogueStack.TryPop(out DialoguePiece result))
        {
            EventHandler.CallShowDialogueEvent(result);
            EventHandler.CallUpdateGameStateEvent(GameState.Pause);
            yield return new WaitUntil(() => result.isDown);
        }
        // 如果首次堆栈中没有数据,那么就先压入
        else
        {
            EventHandler.CallShowDialogueEvent(null);
            EventHandler.CallUpdateGameStateEvent(GameState.GamePlay);

            if (OnFinishEvent != null)
            {
                OnFinishEvent.Invoke();
                canTalk = false;
                if(anchor != null)
                {
                    isActive = anchor.isActive;
                }
            }

            FillDialogueStake();
        }
    }
    /// <summary>
    /// 将列表中的对话压入堆栈中
    /// </summary>
    private void FillDialogueStake()
    {
        dialogueStack = new Stack<DialoguePiece>();
        for (int i = dialogueList.Count - 1; i > -1; i--)
        {
            dialogueList[i].isDown = false;
            dialogueStack.Push(dialogueList[i]);
        }
    }


    // 根据任务不同状态切换不同对话的初步想法
    // 创建一个字典，任务状态为键，对话列表为值
    // 玩家对话时判断当前的任务状态，接着从对话字典SO中抽出当前状态的对话列表
    // 当然是一个NPC一个对话字典
}