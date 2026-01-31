using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxValue = 100;
    private int value = 60;
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
}
