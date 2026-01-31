using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBubble : MonoBehaviour
{
    public float hideDelay = 3f;
    public float fadeDuration = 1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FadeOutAndDestroy fade = GetComponent<FadeOutAndDestroy>();
            if (fade == null)
            {
                fade = gameObject.AddComponent<FadeOutAndDestroy>();
            }
            fade.startFadeDelay = hideDelay;
            fade.fadeDuration = fadeDuration;

        }
    }

}
