using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuibbleVisible : MonoBehaviour
{
    Camera mainCamera;

    public SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public static float getCameraTop(Camera camera)
    {
        if (camera == null) return 0f;
        float height = camera.orthographicSize * 1f;
        float centerY = camera.transform.position.y;
        return height + centerY;
    }

    public static float getCameraBottom(Camera camera)
    {
        if (camera == null) return 0f;
        float height = camera.orthographicSize * 1f;
        float centerY = camera.transform.position.y;
        return centerY - height;
    }

    public static float getSpriteTop(SpriteRenderer sr)
    {
        if (sr == null) return 0f;
        float height = sr.bounds.size.y * 1f /2f;
        float centerY = sr.transform.position.y;
        return height + centerY;
    }

    private void LateUpdate()
    {
        float cameraTop = getCameraTop(mainCamera);
        float spriteTop = getSpriteTop(spriteRenderer);
        float cameraBottom = getCameraBottom(mainCamera);
        setVisible(cameraTop > spriteTop);
        hide(cameraBottom > spriteTop + 1f);

    }   

    private void setVisible(bool visible)
    {
        foreach (Behaviour b in gameObject.GetComponents<Behaviour>())
        {
            if (b != this)
            {
                b.enabled = visible;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = visible;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(visible); // Òþ²Ø×ÓÎïÌå
        }
    }

    private void hide(bool outOfView)
    {
        if (!outOfView)
            {
            return;
        }
        Destroy(gameObject);
    }
}
