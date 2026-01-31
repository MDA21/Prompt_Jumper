using UnityEngine;

public class TrapBubble : MonoBehaviour
{
    public float damagePerSecond = 10f; // 每次触碰扣血量
    public bool isCollider = false;

    private void OnTriggerStay(Collider other)
    {
        // 判断是否是玩家
        isCollider = true;
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            // 持续造成伤害
            player.TakeDamage((int)(damagePerSecond * Time.deltaTime));
        }
    }
}

