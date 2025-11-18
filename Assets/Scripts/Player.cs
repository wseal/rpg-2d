using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }

    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine;


    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.7f;
    [Range(0, 1)]
    public float wallSlideSlowMultiplier = 0.7f;
    private bool isFacingRight = true;
    private int facingDir = 1;

    public Vector2 movementInput { get; private set; }

    [Header("Collision Detection")]
    // [SerializeField] public Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 1.35f;
    [SerializeField] private float wallCheckDistance = 0.55f;
    [SerializeField] public LayerMask groundLayer;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        input = new PlayerInputSet();
        stateMachine = new StateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "JumpFall");
        fallState = new PlayerFallState(this, stateMachine, "JumpFall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
    }

    void OnEnable()
    {
        input.Enable();

        // just begin
        // input.Player.Movement.started += ctx =>
        // {
        //     Vector2 movement = ctx.ReadValue<Vector2>();
        //     Debug.Log($"Movement Input: {movement}");
        // };

        // 
        input.Player.Movement.performed += ctx =>
        {
            // Debug.Log("Movement Input Performed");
            movementInput = input.Player.Movement.ReadValue<Vector2>();
        };


        input.Player.Movement.canceled += ctx =>
        {
            // Debug.Log("Movement Input Canceled");
            movementInput = Vector2.zero;
        };
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !isFacingRight || xVelocity < 0 && isFacingRight)
        {
            Flip();
        }
    }
    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        // Vector3 scale = transform.localScale;
        // scale.x *= -1;
        // transform.localScale = scale;
        isFacingRight = !isFacingRight;
        facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * facingDir * wallCheckDistance);
    }
}
