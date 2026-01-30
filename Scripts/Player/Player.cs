using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine;

    public Sprite normalSprite;
    public Sprite stableSprite;
    public Sprite crazySprite;
    public Sprite invisibleSprite;
    
    [Header("Movement Details")]
    public float moveSpeed = 5f;
    [FormerlySerializedAs("jumpSpeed")] public float jumpForce = 7f;
    public float _moveDir {get; private set;}
    
    [SerializeField]public bool groundDetected { get; private set; }
    

    public Player_NormalState normalState { get; private set; }
    public Player_StableState stableState { get; private set; }
    public Player_CrazyState crazyState { get; private set; }
    public Player_InvisibleState invisibleState { get; private set; }
    
    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance = 0.55f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField]private LayerMask whatIsGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
        input = new PlayerInputSet();
        stateMachine = new StateMachine();
        
        normalState = new Player_NormalState(this, stateMachine, "normal");
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
    }

    private void OnDisable()
    {
        if (input != null)
        {
            // 注销Move动作的回调
            input.Player.Move.performed -= OnMovePerformed;
            input.Player.Move.canceled -= OnMoveCanceled;
            input.Player.Jump.performed -= OnJumpPerformed;
            
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

    private void Start()
    {
        stateMachine.Initialize(normalState);
    }

    private void Update()
    {
        MoveLogic();
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }
    
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
    
    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }

    

}
