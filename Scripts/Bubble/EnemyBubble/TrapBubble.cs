using UnityEngine;

public class TrapBubble : MonoBehaviour
{
    private int damage= 20; // ÿ�δ�����Ѫ��

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}

