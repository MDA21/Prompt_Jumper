using UnityEngine;

public class TrapBubble : MonoBehaviour
{
    private int damage= 30; // ÿ�δ�����Ѫ��

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("Player entered trap bubble area.");
            player.TakeDamage(damage);
        }
    }
}

