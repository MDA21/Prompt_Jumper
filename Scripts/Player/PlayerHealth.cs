using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxValue = 100;
    public int value = 100;
    public UIManager ui;

    private void Awake()
    {
        if (ui == null) ui = FindObjectOfType<UIManager>();
        UpdateUI();
    }

    public void Set(int v)
    {
        value = Mathf.Clamp(v, 0, maxValue);
        UpdateUI();
    }

    public void Add(int delta)
    {
        Set(value + delta);
    }
    
    public void TakeDamage(int amount)
    {
        amount = Mathf.Abs(amount);
        Add(-amount);
    }

    private void UpdateUI()
    {
        if (ui != null) ui.UpdateHappiness(value);
    }
}
