using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PixelTextEditorWindow : EditorWindow
{
    private string inputText = "PROMPT JUMPER";
    private Vector2 startPosition = new Vector2(-8f, 3f);
    private float pixelSize = 0.18f;
    private float letterSpacing = 2.6f;
    private Color pixelColor = Color.white;

    [MenuItem("Tools/Pixel Art/Pixel Text Generator")]
    public static void Open()
    {
        GetWindow<PixelTextEditorWindow>("Pixel Text Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Pixel Text Settings (10x14)", EditorStyles.boldLabel);

        inputText = EditorGUILayout.TextField("Text", inputText);
        startPosition = EditorGUILayout.Vector2Field("Start Position", startPosition);
        pixelSize = EditorGUILayout.FloatField("Pixel Size", pixelSize);
        letterSpacing = EditorGUILayout.FloatField("Letter Spacing", letterSpacing);
        pixelColor = EditorGUILayout.ColorField("Pixel Color", pixelColor);

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Pixel Text"))
        {
            GenerateText(
                inputText.ToUpper(),
                startPosition,
                $"PIXEL_TEXT_{inputText}"
            );
        }
    }

    // ================= 核心生成函数（你要改的就是它） =================
    private void GenerateText(string text, Vector2 startPos, string rootName)
    {
        GameObject root = new GameObject(rootName);
        Vector2 cursor = startPos;

        foreach (char c in text)
        {
            if (c == ' ')
            {
                cursor.x += letterSpacing;
                continue;
            }

            if (!Font10x14.ContainsKey(c))
            {
                Debug.LogWarning($"Unsupported char: {c}");
                continue;
            }

            DrawChar(c, cursor, root.transform);
            cursor.x += letterSpacing;
        }

        Selection.activeGameObject = root;
    }

    private void DrawChar(char c, Vector2 origin, Transform parent)
    {
        string[] glyph = Font10x14[c];

        for (int y = 0; y < glyph.Length; y++)
        {
            for (int x = 0; x < glyph[y].Length; x++)
            {
                if (glyph[y][x] == '#')
                {
                    Vector2 pos = origin + new Vector2(
                        x * pixelSize,
                        -y * pixelSize
                    );

                    CreatePixel(pos, parent);
                }
            }
        }
    }

    private void CreatePixel(Vector2 position, Transform parent)
    {
        GameObject pixel = new GameObject("Pixel");
        pixel.transform.position = position;
        pixel.transform.localScale = Vector3.one * pixelSize;
        pixel.transform.SetParent(parent);

        SpriteRenderer sr = pixel.AddComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(
            Texture2D.whiteTexture,
            new Rect(0, 0, 1, 1),
            new Vector2(0.5f, 0.5f),
            1
        );
        sr.color = pixelColor;
    }

    // ================= 10x14 字库（A–Z 完整） =================
    private static readonly Dictionary<char, string[]> Font10x14 =
        new Dictionary<char, string[]>
    {
        { 'A', new[]{
            "   ####   ",
            "  #    #  ",
            " #      # ",
            " #      # ",
            " ######## ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
        }},
        { 'B', new[]{
            " #######  ",
            " #      # ",
            " #      # ",
            " #######  ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #######  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
        }},
        { 'C', new[]{
            "  ######  ",
            " #      # ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #      # ",
            "  ######  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
        }},
        { 'D', new[]{
            " #######  ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #######  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
        }},
        { 'E', new[]{
            " ######## ",
            " #        ",
            " #        ",
            " #        ",
            " ######## ",
            " #        ",
            " #        ",
            " #        ",
            " ######## ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
        }},
        { 'F', new[]{
            " ######## ",
            " #        ",
            " #        ",
            " ######## ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'G', new[]{
            "  ######  ",
            " #      # ",
            " #        ",
            " #        ",
            " #  ####  ",
            " #      # ",
            " #      # ",
            " #      # ",
            "  ######  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'H', new[]{
            " #      # ",
            " #      # ",
            " #      # ",
            " ######## ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'I', new[]{
            " ######## ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            " ######## ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'J', new[]{
            " ######## ",
            "      ##  ",
            "      ##  ",
            "      ##  ",
            "      ##  ",
            "      ##  ",
            " #    ##  ",
            "  ####    ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'K', new[]{
            " #     #  ",
            " #    #   ",
            " #   #    ",
            " ####     ",
            " #   #    ",
            " #    #   ",
            " #     #  ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'L', new[]{
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " ######## ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'M', new[]{
            " #      # ",
            " ##    ## ",
            " # #  # # ",
            " #  ##  # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'N', new[]{
            " #      # ",
            " ##     # ",
            " # #    # ",
            " #  #   # ",
            " #   #  # ",
            " #    # # ",
            " #     ## ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'O', new[]{
            "  ######  ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            "  ######  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'P', new[]{
            " #######  ",
            " #      # ",
            " #      # ",
            " #######  ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'Q', new[]{
            "  ######  ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #   #  # ",
            " #    ##  ",
            "  #### #  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'R', new[]{
            " #######  ",
            " #      # ",
            " #      # ",
            " #######  ",
            " #   #    ",
            " #    #   ",
            " #     #  ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'S', new[]{
            "  ######  ",
            " #        ",
            " #        ",
            "  #####   ",
            "       #  ",
            "       #  ",
            " #      # ",
            "  ######  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'T', new[]{
            " ######## ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'U', new[]{
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            " #      # ",
            "  ######  ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'V', new[]{
            " #      # ",
            " #      # ",
            " #      # ",
            "  #    #  ",
            "  #    #  ",
            "   #  #   ",
            "   #  #   ",
            "    ##    ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'W', new[]{
            " #      # ",
            " #      # ",
            " #      # ",
            " #  ##  # ",
            " # #  # # ",
            " ##    ## ",
            " #      # ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'X', new[]{
            " #      # ",
            "  #    #  ",
            "   #  #   ",
            "    ##    ",
            "    ##    ",
            "   #  #   ",
            "  #    #  ",
            " #      # ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'Y', new[]{
            " #      # ",
            "  #    #  ",
            "   #  #   ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "    ##    ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
            { 'Z', new[]{
            " ######## ",
            "       #  ",
            "      #   ",
            "     #    ",
            "    #     ",
            "   #      ",
            "  #       ",
            " ######## ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            "          ",
            }},
    };
}
