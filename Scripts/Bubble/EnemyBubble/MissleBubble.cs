using UnityEngine;

public class MissileBubble : MonoBehaviour
{
    private float speedX = 1f; // 水平移动最大速度
    private float speedY = 0.2f; // 垂直移动最大速度

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
        float targetY = player.transform.position.y;

        // 按 speed 限制移动距离
        float newX = Mathf.MoveTowards(currentPos.x, targetX, speedX * Time.fixedDeltaTime);
        float newY = Mathf.MoveTowards(currentPos.y, targetY, speedY * Time.fixedDeltaTime);

        // 设置新的位置，只改变 X
        rb.MovePosition(new Vector2(newX, newY));
    }
}
