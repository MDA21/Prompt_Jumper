using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float minSpeed = 0.5f;
    public float baseSpeed = 1.0f;
    public float maxSpeed = 1.8f;
    public float lerpDuration = 0.2f;
    public float bottomPauseSeconds = 1f;
    public float topLockSeconds = 5f;
    private float currentSpeed;
    private float targetSpeed;
    private float bottomPauseTimer;
    private float topLockTimer;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        currentSpeed = baseSpeed;
        targetSpeed = baseSpeed;
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        if (player != null && cam != null)
        {
            float h = cam.orthographicSize * 2f;
            float bottom = transform.position.y - cam.orthographicSize;
            float top = transform.position.y + cam.orthographicSize;
            float b1 = bottom + h / 3f;
            float b2 = bottom + 2f * h / 3f;
            float py = player.position.y;

            if (py <= bottom && bottomPauseTimer <= 0f)
            {
                bottomPauseTimer = bottomPauseSeconds;
            }

            if (py >= b2 && topLockTimer <= 0f)
            {
                topLockTimer = topLockSeconds;
            }

            if (bottomPauseTimer > 0f)
            {
                targetSpeed = 0f;
            }
            else if (topLockTimer > 0f)
            {
                targetSpeed = maxSpeed;
            }
            else
            {
                if (py < b1) targetSpeed = minSpeed;
                else if (py < b2) targetSpeed = baseSpeed;
                else targetSpeed = maxSpeed;
            }
        }

        if (bottomPauseTimer > 0f) bottomPauseTimer = Mathf.Max(0f, bottomPauseTimer - dt);
        if (topLockTimer > 0f) topLockTimer = Mathf.Max(0f, topLockTimer - dt);

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, dt / Mathf.Max(0.0001f, lerpDuration));
        transform.position += Vector3.up * currentSpeed * dt;
    }
}
