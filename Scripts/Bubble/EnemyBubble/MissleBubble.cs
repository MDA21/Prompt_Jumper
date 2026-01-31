using UnityEngine;

public class MissileBubble : MonoBehaviour
{
    public float speed = 5f;
    public Transform player;

    private void Update()
    {
        if (player == null) return;

        // 计算方向，只左右移动
        Vector3 dir = player.position - transform.position;
        dir.y = 0; // 保持 Y 不动

        // 移动
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
