using UnityEngine;

public class TrapBubble : MonoBehaviour
{
    public int damage= 10; // Ã¿´Î´¥Åö¿ÛÑªÁ¿

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

