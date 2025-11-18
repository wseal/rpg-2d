using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }

    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine;


    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.7f;
    private bool isFacingRight = true;

    public Vector2 movementInput { get; private set; }

    [Header("Collision Detection")]
    // [SerializeField] public Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] public LayerMask groundLayer;
    public bool isGrounded { get; private set; }

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
    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        // Vector3 scale = transform.localScale;
        // scale.x *= -1;
        // transform.localScale = scale;
        isFacingRight = !isFacingRight;
    }

    private void HandleCollisionDetection()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
