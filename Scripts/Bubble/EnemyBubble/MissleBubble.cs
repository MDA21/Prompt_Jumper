using UnityEngine;

public class MissileBubble : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 2f; // 水平移动最大速度（单位：单位/秒）

    public GameObject player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        // 确保是 Kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // 当前水平位置
        Vector2 currentPos = rb.position;

        // 目标水平位置
        float targetX = player.transform.position.x;

        // 按 speed 限制移动距离
        float newX = Mathf.MoveTowards(currentPos.x, targetX, speed * Time.fixedDeltaTime);

        // 设置新的位置，只改变 X
        rb.MovePosition(new Vector2(newX, currentPos.y));
    }
}
