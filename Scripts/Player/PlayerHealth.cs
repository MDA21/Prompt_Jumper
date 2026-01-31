using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxValue = 100;
    private int value = 60;
    public UIManager ui;
    private Player player;
    public int regenPerSecond = 5;
    private float regenTimer;
    private Coroutine hurtRoutine;

    private void Awake()
    {
        if (ui == null) ui = FindObjectOfType<UIManager>();
        player = GetComponent<Player>();
        UpdateUI();
    }

    public void Set(int v)
    {
        value = Mathf.Clamp(v, 0, maxValue);
        UpdateUI();
        if (value == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Add(int delta)
    {
        Set(value + delta);
    }
    
    public void TakeDamage(int amount)
    {
        amount = Mathf.Abs(amount);
        if (player != null && player.IsInvisible) return;
        Add(-amount);
        TriggerHurtEffect();
    }
    
    public void Cost(int amount)
    {
        amount = Mathf.Abs(amount);
        if (player != null && player.IsInvisible) return;
        Add(-amount);
    }

    public void Heal(int amount)
    {
        value = Mathf.Clamp(value + amount, 0, maxValue);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ui != null) ui.UpdateHappiness(value);
    }

    private void Update()
    {
        regenTimer += Time.deltaTime;
        while (regenTimer >= 1f)
        {
            if (player == null || !player.IsInvisible)
            {
                Add(regenPerSecond);
            }
            regenTimer -= 1f;
        }
    }

    private void TriggerHurtEffect()
    {
        if (player == null || player.sr == null) return;
        if (player.IsInvisible) return;
        if (hurtRoutine != null) StopCoroutine(hurtRoutine);
        hurtRoutine = StartCoroutine(HurtFlashRoutine());
    }

    private IEnumerator HurtFlashRoutine()
    {
        var baseSr = player.sr;
        var overlay = new GameObject("HurtOverlay");
        overlay.transform.SetParent(player.transform);
        overlay.transform.localPosition = Vector3.zero;
        var osr = overlay.AddComponent<SpriteRenderer>();
        if (player.hurtSprite == null)
        {
            Object.Destroy(overlay);
            yield break;
        }
        osr.sprite = player.hurtSprite;
        osr.color = new Color(1f, 0f, 0f, 0.25f);
        osr.sortingLayerID = baseSr.sortingLayerID;
        osr.sortingOrder = baseSr.sortingOrder + 1;
        
        if (osr.sprite != null && baseSr.sprite != null)
        {
            Vector2 targetSize = baseSr.sprite.bounds.size;
            Vector2 baseSize = osr.sprite.bounds.size;
            float scaleX = baseSize.x > 0f ? targetSize.x / baseSize.x : 1f;
            float scaleY = baseSize.y > 0f ? targetSize.y / baseSize.y : 1f;
            overlay.transform.localScale = new Vector3(scaleX, scaleY, 1f);
        }
        
        yield return new WaitForSeconds(0.12f);
        Object.Destroy(overlay);
        hurtRoutine = null;
    }
}
