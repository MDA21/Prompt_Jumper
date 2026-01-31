using UnityEngine;

public class TrapBubble : MonoBehaviour
{
    public int damage = 10;              // Ã¿´Î´¥Åö¿ÛÑªÁ¿
    public float damageInterval = 1f;    // ¿ÛÑª¼ä¸ô£¨Ãë£©

    private float lastDamageTime = 0f;

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("TrapBubble collided with " + collision.gameObject.name);
        // ¼ì²âÍæ¼Ò
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                player.TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }

    }
}

