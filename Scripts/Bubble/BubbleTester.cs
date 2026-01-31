using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTester : MonoBehaviour
{
    public BubbleView bubblePrefab;

    private void Start()
    {
       Spawn("Hello, World!", new Vector3(-3, 2, 0), BubbleType.Mine);
        Spawn("This is a longer message to test the bubble sizing functionality.", new Vector3(10, 0, 0), BubbleType.Trap);
        Spawn("Short msg", new Vector3(3, -3, 0), BubbleType.Missile);
    }

    void Spawn(string message, Vector3 position, BubbleType type)
    {
        BubbleView bubble = Instantiate(bubblePrefab, position, Quaternion.identity);
        bubble.setBubbleText(message);
    }

}
