using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class BubbleView : MonoBehaviour
{
    [Header("References")]
    public TMP_Text text;

    [Header("Layout")]
    public float maxTextWidth = 4.5f;
    public Vector2 padding = new Vector2(0.6f, 0.4f);

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetText(string message)
    {
        // Set the text
        text.text = message;

        // 限制文本最大宽度
        text.rectTransform.sizeDelta = new Vector2(maxTextWidth, 100f);

        // 计算文本真实排版尺寸
        Vector2 textSize = text.GetPreferredValues();

        // 计算bubble尺寸
        Vector2 bubbleSize = textSize + padding;

        // 调整sprite尺寸
        spriteRenderer.size = bubbleSize;

        // 调整碰撞体尺寸
        boxCollider.size = bubbleSize;
        boxCollider.offset = Vector2.zero;

        // 调整文本位置
        text.transform.localPosition = Vector3.zero;
    }

}
