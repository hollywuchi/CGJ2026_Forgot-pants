using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUI : MonoBehaviour
{
    [Header("预制体")]
    public GameObject doorUI;

    [Header("生成设置")]
    public int spawnCount = 50;
    public float minScale = 0.3f;
    public float maxScale = 1.5f;
    public float spawnInterval = 0.05f;

    private RectTransform panelRect;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void OnEnable()
    {
        panelRect = GetComponent<RectTransform>();
        StartCoroutine(SpawnDoorUI());
    }

    void OnDisable()
    {
        ClearSpawnedObjects();
    }

    private IEnumerator SpawnDoorUI()
    {
        yield return null;

        Vector2 panelSize = panelRect.rect.size;
        float panelWidth = panelSize.x;
        float panelHeight = panelSize.y;

        for (int i = 0; i < spawnCount; i++)
        {
            if (doorUI == null) break;

            GameObject obj = Instantiate(doorUI, transform);
            obj.name = $"DoorUI_{i}";

            RectTransform objRect = obj.GetComponent<RectTransform>();
            if (objRect != null)
            {
                float randomX = Random.Range(-panelWidth / 2, panelWidth / 2);
                float randomY = Random.Range(-panelHeight / 2, panelHeight / 2);
                objRect.anchoredPosition = new Vector2(randomX, randomY);

                float randomScale = Random.Range(minScale, maxScale);
                objRect.localScale = new Vector3(randomScale, randomScale, 1f);

                objRect.anchorMin = new Vector2(0.5f, 0.5f);
                objRect.anchorMax = new Vector2(0.5f, 0.5f);
                objRect.pivot = new Vector2(0.5f, 0.5f);
            }

            spawnedObjects.Add(obj);

            if (spawnInterval > 0)
            {
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    private void ClearSpawnedObjects()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();
    }
}
