using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine;

    // public Sprite normalSprite;
    public Sprite stableSprite;
    public Sprite crazySprite;
    public Sprite invisibleSprite;
    public Sprite hurtSprite;
    
    [Header("Bubble Prefabs")]
    public List<GameObject> stableBubblePrefabs;
    public List<GameObject> crazyBubblePrefabs;
    public GameObject withdrawMessagePrefab;

    [Header("Interaction Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 13f;
    public float _moveDir {get; private set;}
    public float crazyBubbleBounceForce = 15f;
    
    public bool groundDetected;
    public bool crazyDetected;

    public PlayerHealth Health { get; private set; }

    // public Player_NormalState normalState { get; private set; }
    public Player_StableState stableState { get; private set; }
    public Player_CrazyState crazyState { get; private set; }
    public Player_InvisibleState invisibleState { get; private set; }
    
    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance = 0.55f;
    //[SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField]private LayerMask whatIsGround;
    [SerializeField]private LayerMask whatIsCrazy;
    private bool canBounce = true;

    public int playerCrazyCost = 20;
    public int playerStableCost = 15;
    
    public bool IsInvisible
    {
        get
        {
            return stateMachine != null && stateMachine.CurrentState == invisibleState;
        }
    }
    
    [Header("Stable Bubble Limit")]
    public int maxStableBubbles = 3;
    private List<GameObject> activeStableBubbles = new List<GameObject>();

    // Audio related
    private bool wasGrounded;

    public void RegisterStableBubble(GameObject bubble)
    {
        if (bubble == null) return;
        activeStableBubbles.RemoveAll(b => b == null);
        activeStableBubbles.Add(bubble);
        while (activeStableBubbles.Count > maxStableBubbles)
        {
            var oldest = activeStableBubbles[0];
            activeStableBubbles.RemoveAt(0);
            if (oldest != null) Destroy(oldest);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Health = GetComponent<PlayerHealth>();
        
        input = new PlayerInputSet();
        stateMachine = new StateMachine();
        
        // normalState = new Player_NormalState(this, stateMachine, "normal");
        stableState = new Player_StableState(this, stateMachine, "stable");
        crazyState = new Player_CrazyState(this, stateMachine, "crazy");
        invisibleState = new Player_InvisibleState(this, stateMachine, "invisible");

    }

    private void OnEnable()
    {
        if (input == null) input = new PlayerInputSet();

        input.Enable();
        input.Player.Move.performed += OnMovePerformed;
        input.Player.Move.canceled += OnMoveCanceled;
        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Change.performed += OnChangePerformed;
        input.Player.Skill.performed += OnSkillPerformed;
        input.Player.Invisible.performed += OnInvisiblePerformed;
        

    }

    private void OnDisable()
    {
        if (input != null)
        {
            // 注销Move动作的回调
            input.Player.Move.performed -= OnMovePerformed;
            input.Player.Move.canceled -= OnMoveCanceled;
            input.Player.Jump.performed -= OnJumpPerformed;
            input.Player.Change.performed -= OnChangePerformed;
            input.Player.Skill.performed -= OnSkillPerformed;
            input.Player.Invisible.performed -= OnInvisiblePerformed;
            
            input.Disable();
        }
    }
    
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<float>();
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveDir = 0;
    }

    public void MoveLogic()
    {
        
        float targetXVelocity = _moveDir * moveSpeed;
        SetVelocity(targetXVelocity, rb.velocity.y);
    }
    
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Jump触发！按下了跳跃键");
        stateMachine.CurrentState.Jump();
    }
    
    private void OnChangePerformed(InputAction.CallbackContext context)
    {
        CycleBetweenBasicStates();
    }
    
    private void CycleBetweenBasicStates()
    {
        if (stateMachine.CurrentState == stableState)
        {
            stateMachine.ChangeState(crazyState);
        }
        else if (stateMachine.CurrentState == crazyState)
        {
            stateMachine.ChangeState(stableState);
        }

    }

    private void OnSkillPerformed(InputAction.CallbackContext context)
    {
        // Debug.Log("Skill Performed");
        if (stateMachine != null && stateMachine.CurrentState != null)
        {
            stateMachine.CurrentState.Skill();
        }
    }

    private void OnInvisiblePerformed(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(invisibleState);
    }
    
    private void Start()
    {
        stateMachine.Initialize(stableState);
    }

    private void Update()
    {
        MoveLogic();
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
        
        CheckLandAudio();
    }
    
    private void CheckLandAudio()
    {
        if (!wasGrounded && groundDetected)
        {
            // Just landed
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.Play("Land");
            }
        }
        wasGrounded = groundDetected;
    }
    
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
    
    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        
        RaycastHit2D crazyHit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsCrazy);
        crazyDetected = crazyHit;

    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }

    public void StartCrazyBubbleRoutine(GameObject bubble)
    {
        StartCoroutine(CrazyBubbleRoutine(bubble));
    }

    private IEnumerator CrazyBubbleRoutine(GameObject bubble)
    {
        yield return new WaitForSeconds(3f);
        if (bubble != null)
        {
            Vector3 pos = bubble.transform.position;
            Destroy(bubble);
            if (withdrawMessagePrefab != null)
            {
                Instantiate(withdrawMessagePrefab, pos, Quaternion.identity);
            }
        }
    }

    public void StartInvisibleTimer()
    {
        StartCoroutine(InvisibleTimer());
    }

    private IEnumerator InvisibleTimer()
    {
        yield return new WaitForSeconds(5.7f);
        if (stateMachine.CurrentState == invisibleState)
        {
            stateMachine.ChangeState(stableState);
        }
    }

    public void SetTransparency(float alpha)
    {
        if (sr != null)
        {
            Color color = sr.color;
            color.a = alpha;
            sr.color = color;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (crazyDetected && canBounce)
        {
            canBounce = false;
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, 0f));
            rb.AddForce(direction * crazyBubbleBounceForce, ForceMode2D.Impulse);
            StartCoroutine(ResetBounce());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!crazyDetected)
        {
            canBounce = true;
        }
    }

    private IEnumerator ResetBounce()
    {
        yield return new WaitForSeconds(0.2f);
        canBounce = true;
    }

    

}
