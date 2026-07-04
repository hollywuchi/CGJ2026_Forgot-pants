using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 声音预制体 (替换了原来的 poolPrefabs 列表)
    public GameObject soundPrefab;

    // 队列，当做音效的对象池
    private Queue<GameObject> soundQueue = new Queue<GameObject>();

    void OnEnable()
    {
        EventHandler.InitSoundEffect += OnInitSoundEffect;
    }

    void OnDisable()
    {
        EventHandler.InitSoundEffect -= OnInitSoundEffect;
    }

    void Start()
    {
        // 游戏开始时初始化音效池
        CreatSoundPool();
    }

    /// <summary>
    /// 创建并初始化声音对象池
    /// </summary>
    private void CreatSoundPool()
    {
        Transform parent = new GameObject(soundPrefab.name).transform;
        parent.SetParent(transform);

        for (int i = 0; i < 20; i++)
        {
            GameObject newObj = Instantiate(soundPrefab, parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }

    /// <summary>
    /// 获取对象池中的对象
    /// </summary>
    /// <returns></returns>
    private GameObject GetPoolProgect()
    {
        if (soundQueue.Count < 2)
            CreatSoundPool();
        return soundQueue.Dequeue();
    }

    private void OnInitSoundEffect(SoundDetails details)
    {
        var obj = GetPoolProgect();
        obj.GetComponent<Sound>().SetSound(details);
        obj.SetActive(true);

        StartCoroutine(DisableSound(obj, details.soundClip.length));
    }

    private IEnumerator DisableSound(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
        soundQueue.Enqueue(obj);
    }
}