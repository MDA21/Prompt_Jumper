using UnityEngine;
using UnityEngine.UI; // ����UIѪ��

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill;  // UIѪ����Fill����

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
        // ÿ3s�Ӳ��Կ�Ѫ10��
        if (Time.frameCount % (3 * 60) == 0)
        {
            Debug.Log("��ǰѪ��: " + currentHealth);

        }
    }

    private void Die()
    {
        Debug.Log("���������");
        SceneManager.LoadScene("GameOver");
        // ������Լ���������Ϸ�������߼�
    }
}
