using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class BubbleView : MonoBehaviour
{
    [Header("References")]
    public TMP_Text text;

    [Header("Layout")]
    public float maxTextWidth = 4.5f;
    public int maxTextPerLine = 25;
    public Vector2 padding = new Vector2(0.6f, 0.4f);

    private Transform DialogSquare;
    private Transform Tail;
    private BoxCollider2D col;
    private SpriteRenderer sr;

    void Awake()
    {
        DialogSquare = transform.Find("Dialog");
        Tail = transform.Find("Tail");
        sr = DialogSquare.GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    public void SetText(string message)
    {
        // Set the text
        text.text = message;
        processMessage(message);

        // 限制文本最大宽度
        text.rectTransform.sizeDelta = new Vector2(maxTextWidth, 100f);

        // 计算文本真实排版尺寸
        Vector2 textSize = text.GetPreferredValues();

        // 计算bubble尺寸
        Vector2 bubbleSize = textSize + padding;

        // 调整sprite尺寸
        sr.size = bubbleSize;

        // 调整碰撞体尺寸
        col.size = bubbleSize;
        col.offset = Vector2.zero;

        // 调整文本位置
        text.transform.localPosition = Vector3.zero;

        // 计算左右边界
        float left = -bubbleSize.x / 2f;
        float right = bubbleSize.x / 2f;

        // 调整Tail位置
        float randomX = Random.value < 0.5f ? left : right;
        Tail.localPosition = new Vector3(randomX, 0, 0f);
    }

    void processMessage(string message)
    {
        if (message.Length <= maxTextPerLine)
            return;
        for (int i = maxTextPerLine; i < message.Length; i += maxTextPerLine)
        {
            message = message.Insert(i, "\n");
            i++;
        }
    }
}