using UnityEngine;
using UnityEngine.UI; // 用于UI血条

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill;  // UI血条的Fill对象

    private void Start()
    {
        currentHealth = 60;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Update()
    {
        // 每3s钟测试扣血10点
        if (Time.frameCount % (3 * 60) == 0)
        {
            Debug.Log("当前血量: " + currentHealth);

        }
    }

    private void Die()
    {
        Debug.Log("玩家死亡！");
        // 这里可以加重生、游戏结束等逻辑
    }
}
