using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections.Generic;

public static class BubbleFromTxtGenerator
{
    [MenuItem("Tools/Bubble/Generate From Txt")]
    public static void GenerateFromTxt()
    {
        string txtPath = "Assets/Resources/Data/Data.txt";

        // 1. 准备 prefab 字典
        Dictionary<BubbleType, GameObject> prefabDict = new Dictionary<BubbleType, GameObject>();

        foreach (BubbleType type in System.Enum.GetValues(typeof(BubbleType)))
        {
            string prefabPath = $"Assets/Prefabs/Bubbles/{type}Bubble.prefab";
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab == null)
            {
                Debug.LogError($"Prefab not found for type {type} at path {prefabPath}");
                continue;
            }

            prefabDict[type] = prefab;
        }

        if (!File.Exists(txtPath))
        {
            Debug.LogError("Txt file not found");
            return;
        }

        string[] lines = File.ReadAllLines(txtPath);

        // 2. 父物体
        GameObject root = new GameObject("GeneratedBubbles");
        Undo.RegisterCreatedObjectUndo(root, "Create Bubble Root");

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            // text | posx | posy | type
            string[] parts = line.Split('|');
            if (parts.Length < 4)
            {
                Debug.LogWarning($"Invalid line format: {line}");
                continue;
            }

            string text = parts[0].Trim();
            if (!float.TryParse(parts[1].Trim(), out float x) || !float.TryParse(parts[2].Trim(), out float y))
            {
                Debug.LogWarning($"Invalid position in line: {line}");
                continue;
            }

            if (!System.Enum.TryParse(parts[3].Trim(), true, out BubbleType type))
            {
                Debug.LogWarning($"Invalid BubbleType in line: {line}");
                continue;
            }

            if (!prefabDict.TryGetValue(type, out GameObject prefab))
            {
                Debug.LogWarning($"No prefab found for type {type}, skipping line.");
                continue;
            }

            // 3. 实例化 prefab
            GameObject bubble = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(bubble, "Create Bubble");

            bubble.transform.position = new Vector3(x, y, 0);
            bubble.transform.SetParent(root.transform);

            // 4. 设置文本
            BubbleView view = bubble.GetComponent<BubbleView>();
            if (view != null)
                view.setBubbleText(text);
            else
                Debug.LogWarning($"BubbleView component not found on prefab for type {type}");
        }

        EditorSceneManager.MarkSceneDirty(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene()
        );

        Debug.Log("Bubbles generated from txt.");
    }
}
