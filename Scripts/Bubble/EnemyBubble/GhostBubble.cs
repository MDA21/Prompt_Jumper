using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBubble : MonoBehaviour
{
    public float hideDelay = 20f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 让碰撞到的物体忽略与此物体的碰撞
            StartCoroutine(HideAfterDelay(hideDelay));

        }
    }


    private IEnumerator HideAfterDelay(float time)
        {
            yield return new WaitForSeconds(time*Time.fixedDeltaTime);
            gameObject.SetActive(false);
        }

}
