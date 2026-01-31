using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public static class BubbleFromTxtGenerator
{
    [MenuItem("Tools/Bubble/Generate From Txt")]
    public static void GenerateFromTxt()
    {
        string txtPath = "Assets/Resources/Data/Data.txt";

        // 1. 根据 BubbleType 创建字典映射 prefab 路径
        var prefabDict = new System.Collections.Generic.Dictionary<BubbleType, string>()
        {
            { BubbleType.Trap, "Assets/Prefabs/TrapBubble.prefab" },
            { BubbleType.Block, "Assets/Prefabs/BlockBubble.prefab" },
            { BubbleType.Missile, "Assets/Prefabs/MissileBubble.prefab" },
            { BubbleType.Ghost, "Assets/Prefabs/GhostBubble.prefab" },
        };

        // 2. 读取 txt
        if (!File.Exists(txtPath))
        {
            Debug.LogError("Txt file not found: " + txtPath);
            return;
        }

        string[] lines = File.ReadAllLines(txtPath);

        // 3. 父物体
        GameObject root = new GameObject("GeneratedBubbles");
        Undo.RegisterCreatedObjectUndo(root, "Create Bubble Root");

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            // text|x|y|type
            string[] parts = line.Split('|');

            if (parts.Length < 4)
            {
                Debug.LogWarning("Invalid line: " + line);
                continue;
            }

            string text = parts[0];
            float x = float.Parse(parts[1]);
            float y = float.Parse(parts[2]);
            BubbleType type = (BubbleType)System.Enum.Parse(typeof(BubbleType), parts[3], true);

            // 4. 根据 type 获取 prefab 路径
            if (!prefabDict.TryGetValue(type, out string prefabPath))
            {
                Debug.LogWarning("Prefab not found for type: " + type);
                continue;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab == null)
            {
                Debug.LogError("Prefab missing at path: " + prefabPath);
                continue;
            }

            // 5. 实例化 prefab
            GameObject bubble = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(bubble, "Create Bubble");

            bubble.transform.position = new Vector3(x, y, 0);
            bubble.transform.SetParent(root.transform);

            // 6. 调用 BubbleView 设置文字
            BubbleView view = bubble.GetComponent<BubbleView>();
            if (view != null)
            {
                view.setBubbleText(text);
            }
            else
            {
                Debug.LogWarning("Bubble prefab missing BubbleView component: " + prefab.name);
            }
        }

        // 7. 标记 Scene 可保存
        EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());

        Debug.Log("Bubbles generated from txt.");
    }
}
