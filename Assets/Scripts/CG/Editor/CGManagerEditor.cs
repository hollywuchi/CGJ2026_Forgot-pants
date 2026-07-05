using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class CGManagerEditor : EditorWindow
{
    [MenuItem("Tools/CG Manager")]
    public static void ShowWindow()
    {
        GetWindow<CGManagerEditor>("CG Manager");
    }

    private Vector2 scrollPos;
    private List<CGDataEntry> cgEntries = new List<CGDataEntry>();
    private Sprite defaultCGImage;
    private string newCGName = "";

    [System.Serializable]
    private class CGDataEntry
    {
        public string name;
        public string text;
        public bool foldout;
    }

    void OnEnable()
    {
        LoadExistingCGData();
    }

    private void LoadExistingCGData()
    {
        string[] guids = AssetDatabase.FindAssets("t:CGDialogueList_SO", new[] { "Assets/GameData/CG" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CGDialogueList_SO data = AssetDatabase.LoadAssetAtPath<CGDialogueList_SO>(path);
            if (data != null)
            {
                CGDataEntry entry = new CGDataEntry
                {
                    name = data.name,
                    text = GetTextFromCGData(data),
                    foldout = false
                };
                cgEntries.Add(entry);
            }
        }
    }

    private string GetTextFromCGData(CGDialogueList_SO data)
    {
        List<string> texts = new List<string>();
        foreach (var piece in data.cgList)
        {
            if (!string.IsNullOrEmpty(piece.dialogueText))
            {
                texts.Add(piece.dialogueText);
            }
        }
        return string.Join("\n", texts);
    }

    void OnGUI()
    {
        GUILayout.Label("CG管理器", EditorStyles.boldLabel);

        defaultCGImage = (Sprite)EditorGUILayout.ObjectField("默认CG图片", defaultCGImage, typeof(Sprite), false);

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        newCGName = EditorGUILayout.TextField("新CG名称", newCGName);
        if (GUILayout.Button("添加新CG", GUILayout.Width(100)))
        {
            AddNewCG();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        if (GUILayout.Button("扫描已有CG数据"))
        {
            cgEntries.Clear();
            LoadExistingCGData();
        }

        EditorGUILayout.Space();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        for (int i = 0; i < cgEntries.Count; i++)
        {
            DrawCGEntry(i);
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();
        if (GUILayout.Button("批量生成所有CG数据"))
        {
            GenerateAllCGData();
        }
    }

    private void DrawCGEntry(int index)
    {
        EditorGUILayout.BeginVertical("box");
        
        EditorGUILayout.BeginHorizontal();
        cgEntries[index].foldout = EditorGUILayout.Foldout(cgEntries[index].foldout, cgEntries[index].name, true);
        
        if (GUILayout.Button("删除", GUILayout.Width(60)))
        {
            DeleteCGData(cgEntries[index].name);
            cgEntries.RemoveAt(index);
            return;
        }
        EditorGUILayout.EndHorizontal();

        if (cgEntries[index].foldout)
        {
            cgEntries[index].name = EditorGUILayout.TextField("名称", cgEntries[index].name);
            EditorGUILayout.LabelField("文本内容（每行一句）:");
            cgEntries[index].text = EditorGUILayout.TextArea(cgEntries[index].text, GUILayout.Height(150));
        }

        EditorGUILayout.EndVertical();
    }

    private void AddNewCG()
    {
        if (string.IsNullOrEmpty(newCGName))
        {
            newCGName = $"CG_{cgEntries.Count + 1:D2}";
        }

        CGDataEntry entry = new CGDataEntry
        {
            name = newCGName,
            text = "",
            foldout = true
        };
        cgEntries.Add(entry);
        newCGName = "";
    }

    private void GenerateAllCGData()
    {
        string basePath = "Assets/GameData/CG";
        if (!AssetDatabase.IsValidFolder(basePath))
        {
            AssetDatabase.CreateFolder("Assets/GameData", "CG");
        }

        foreach (var entry in cgEntries)
        {
            CGDialogueList_SO cgData = ScriptableObject.CreateInstance<CGDialogueList_SO>();
            
            string[] lines = entry.text.Split('\n');
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    CGPiece piece = new CGPiece
                    {
                        dialogueText = trimmedLine,
                        displayDuration = 3f,
                        waitForInput = true
                    };
                    cgData.cgList.Add(piece);
                }
            }

            string assetPath = $"{basePath}/{entry.name}.asset";
            
            CGDialogueList_SO existingAsset = AssetDatabase.LoadAssetAtPath<CGDialogueList_SO>(assetPath);
            if (existingAsset != null)
            {
                EditorUtility.CopySerialized(cgData, existingAsset);
                EditorUtility.SetDirty(existingAsset);
            }
            else
            {
                AssetDatabase.CreateAsset(cgData, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"已生成 {cgEntries.Count} 个CG数据资产");
    }

    private void DeleteCGData(string cgName)
    {
        string assetPath = $"Assets/GameData/CG/{cgName}.asset";
        if (AssetDatabase.LoadAssetAtPath<CGDialogueList_SO>(assetPath) != null)
        {
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.SaveAssets();
        }
    }
}
