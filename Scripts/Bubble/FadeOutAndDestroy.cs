using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAndDestroy : MonoBehaviour
{
    public float startFadeDelay = 1f;
    public float fadeDuration = 1f;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(startFadeDelay);

        float timer = 0f;
        Color startColor = sr.color;
        
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0f, timer / fadeDuration);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
