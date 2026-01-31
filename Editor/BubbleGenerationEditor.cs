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
        string prefabPath = "Assets/Prefabs/Bubble.prefab";

        // 1. 读取 prefab
        GameObject prefab =
            AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError("Bubble prefab not found");
            return;
        }

        // 2. 读取 txt
        if (!File.Exists(txtPath))
        {
            Debug.LogError("Txt file not found");
            return;
        }

        string[] lines = File.ReadAllLines(txtPath);

        // 3. 父物体（可选，但非常推荐）
        GameObject root = new GameObject("GeneratedBubbles");
        Undo.RegisterCreatedObjectUndo(root, "Create Bubble Root");

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            // text|x|y|type
            string[] parts = line.Split('|');

            string text = parts[0];
            float x = float.Parse(parts[1]);
            float y = float.Parse(parts[2]);
            BubbleType type =
                (BubbleType)System.Enum.Parse(
                    typeof(BubbleType), parts[3], true);

            // 4. 实例化 prefab（关键）
            GameObject bubble =
                (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            Undo.RegisterCreatedObjectUndo(bubble, "Create Bubble");

            bubble.transform.position = new Vector3(x, y, 0);
            bubble.transform.SetParent(root.transform);

            // 5. 调用你已有的逻辑
            BubbleView view = bubble.GetComponent<BubbleView>();
            view.setBubbleText(text);
        }

        // 6. 标记 Scene 可保存
        EditorSceneManager.MarkSceneDirty(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene()
        );

        Debug.Log("Bubbles generated from txt.");
    }
}
