using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBubble : MonoBehaviour
{
    public BubbleType type = BubbleType.Trap;
    public int trapDamage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<PlayerHealth>();
        if (health == null) return;
        if (type == BubbleType.Trap)
        {
            health.TakeDamage(trapDamage);
        }
    }
}
