using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleView : MonoBehaviour
{
    [Header("References")]
    public TMP_Text text;

    [Header("Layout")]
    public float maxTextWidth = 4.5f;
    public int maxTextPerLine = 25;
    public Vector2 padding = new Vector2(0.6f, 0.4f);



    private Transform Tail;
    private BoxCollider2D col;
    private SpriteRenderer sr;

    void Awake()
    {
        Tail = transform.Find("Tail");
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    public void InitIfNeeded()
    {
        
       Tail = transform.Find("Tail");

        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (text == null)
            text = GetComponentInChildren<TMP_Text>();
    }


    public void setBubbleText(string message)
    {
        InitIfNeeded();


        message = processMessage(message);
        text.text = message;

        text.rectTransform.sizeDelta = new Vector2(maxTextWidth, 100f);

        text.ForceMeshUpdate();
        Bounds bounds = text.bounds;

        Vector2 bubbleSize = new Vector2(
            bounds.size.x + padding.x,
            bounds.size.y + padding.y
        );

        sr.size = bubbleSize;
        col.size = bubbleSize;
        col.offset = Vector2.zero;

        text.transform.localPosition = Vector3.zero;

        float left = -bubbleSize.x / 2f;
        float X = left;


        Tail.localPosition = new Vector3(X, 0f, 0f);
        //Color color = setColor(type);
        //Tail.GetComponent<SpriteRenderer>().color = color;
        //sr.color = color;
    }


    private string processMessage(string message)
    {
        if (message.Length <= maxTextPerLine)
            return message;
        for (int i = maxTextPerLine; i < message.Length; i += maxTextPerLine)
        {
            message = message.Insert(i, "\n");
            i++;
        }
        return message;
    }

    //private Color setColor(BubbleType type)
    //{
    //    switch (type)
    //    {
    //        case BubbleType.Trap:
    //            return Color.red;

    //        case BubbleType.Missile:
    //            return new Color(1f, 0.6f, 0f);   // ³È

    //        case BubbleType.Mine:
    //            return Color.green;

    //        case BubbleType.Block:
    //            return Color.white;

    //        case BubbleType.Ghost:
    //            return Color.gray;

    //        default:
    //            return Color.white;
    //    }
    //}

}