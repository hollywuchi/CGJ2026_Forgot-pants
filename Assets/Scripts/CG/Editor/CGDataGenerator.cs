using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class CGDataGenerator : EditorWindow
{
    [MenuItem("Tools/CG Data Generator")]
    public static void ShowWindow()
    {
        GetWindow<CGDataGenerator>("CG Data Generator");
    }

    private string cgName = "CG_01";
    private string cgText = @"不是你的错。
丁忆：老哥，别伤心。
丁一：……
丁忆：看。床上没有鱼。家里没有鸟。
丁一：……
丁忆：月亮是明亮的黄色，星星是晶莹的白色。
丁一：……
丁忆摊开手心。
丁忆：老哥，有它在，我就能复活。
丁忆的手心中出现一枚结晶，模样正是我在迷宫中一直抓在手里的东西。那是一颗锚点，锚点的透明心脏里闪回着什么。
我仙鹤一样伸长脖子，努力想要看清，但无论我怎样努力，锚点中心都有一层雾气，朦胧到让人看不见里面的回忆。
我回家了，坐在沙发上，一切都像一场梦。
丁忆的话语随着时间一点点淡去，我拿起笔记本想要记录关于迷宫的事，提起笔却只写出几个字。
【心迷宫】。
……
我给丁忆烧了很多纸，足够地府通货膨胀了。
我的身后再也没有幽灵。";

    private float displayDuration = 3f;
    private bool waitForInput = true;

    void OnGUI()
    {
        GUILayout.Label("CG数据生成器", EditorStyles.boldLabel);
        
        cgName = EditorGUILayout.TextField("CG名称", cgName);
        EditorGUILayout.LabelField("CG文本（每行一句）:");
        cgText = EditorGUILayout.TextArea(cgText, GUILayout.Height(300));
        displayDuration = EditorGUILayout.FloatField("显示时长(秒)", displayDuration);
        waitForInput = EditorGUILayout.Toggle("等待玩家输入", waitForInput);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("提示：默认图片会应用到所有CG帧。如需单独设置某帧图片，可在生成后的资产中修改。", MessageType.Info);
        
        if (GUILayout.Button("生成CG数据资产"))
        {
            GenerateCGData();
        }
    }

    private void GenerateCGData()
    {
        CGDialogueList_SO cgData = ScriptableObject.CreateInstance<CGDialogueList_SO>();
        
        string[] lines = cgText.Split('\n');
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (!string.IsNullOrEmpty(trimmedLine))
            {
                CGPiece piece = new CGPiece
                {
                    dialogueText = trimmedLine,
                    displayDuration = displayDuration,
                    waitForInput = waitForInput
                };
                cgData.cgList.Add(piece);
            }
        }

        string path = "Assets/GameData/CG";
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/GameData", "CG");
        }

        string assetPath = $"{path}/{cgName}.asset";
        AssetDatabase.CreateAsset(cgData, assetPath);
        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = cgData;
        
        Debug.Log($"CG数据资产已生成: {assetPath}，共{cgData.cgList.Count}条数据");
    }
}
