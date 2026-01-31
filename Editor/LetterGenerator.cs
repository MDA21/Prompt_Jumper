using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class PixelTextEditorGenerator
{
    // ================== 可调参数 ==================
    private static float pixelSize = 0.18f;                 // 单个像素尺寸
    private static float letterSpacing = 2.6f;              // 字符间距
    private static Vector2 startPosition = new Vector2(-8f, 3f); // 起始世界坐标
    private static Color pixelColor = Color.white;

    // ================== 菜单入口 ==================
    [MenuItem("Tools/Pixel Art/Generate PROMPT JUMPER (10x14)")]
    public static void GeneratePromptJumper()
    {
        string text = "PROMPT JUMPER";

        GameObject root = new GameObject("PROMPT_JUMPER_PixelText");

        Vector2 cursor = startPosition;

        foreach (char c in text)
        {
            if (c == ' ')
            {
                cursor.x += letterSpacing;
                continue;
            }

            DrawChar(c, cursor, root.transform);
            cursor.x += letterSpacing;
        }

        Selection.activeGameObject = root;
    }

    // ================== 绘制字符 ==================
    private static void DrawChar(char c, Vector2 origin, Transform parent)
    {
        if (!Font10x14.ContainsKey(c))
        {
            Debug.LogWarning($"Missing pixel font for: {c}");
            return;
        }

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

    // ================== 创建像素 ==================
    private static void CreatePixel(Vector2 position, Transform parent)
    {
        GameObject pixel = new GameObject("Pixel");
        pixel.transform.position = position;
        pixel.transform.localScale = Vector3.one * pixelSize;
        pixel.transform.SetParent(parent);

        SpriteRenderer sr = pixel.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = pixelColor;
        sr.sortingOrder = 0;
    }

    // ================== 获取 Square Sprite ==================
    private static Sprite GetSquareSprite()
    {
        Texture2D tex = Texture2D.whiteTexture;
        return Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f),
            100
        );
    }

    // ================== 10x14 像素字库 ==================
    private static readonly Dictionary<char, string[]> Font10x14 =
        new Dictionary<char, string[]>
    {
        { 'P', new[]{
            " ######## ",
            " #      # ",
            " #      # ",
            " #      # ",
            " ######## ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            " #        ",
            "          ",
            "          ",
        }},
        { 'R', new[]{
            " ######## ",
            " #      # ",
            " #      # ",
            " #      # ",
            " ######## ",
            " #   #    ",
            " #    #   ",
            " #     #  ",
            " #      # ",
            " #      # ",
            " #      # ",
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
            " #      # ",
            " #      # ",
            " #      # ",
            "  ######  ",
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
            " #      # ",
            " #      # ",
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
            "    ##    ",
            "    ##    ",
            "    ##    ",
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
            "      ##  ",
            "      ##  ",
            " #    ##  ",
            " #    ##  ",
            "  ####    ",
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
            " #      # ",
            " #      # ",
            " #      # ",
            "  ######  ",
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
            " #        ",
            " #        ",
            " ######## ",
            "          ",
            "          ",
            "          ",
        }},
    };
}
