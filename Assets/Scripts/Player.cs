using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public Vector2 movementInput { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private PlayerInputSet input;
    private StateMachine stateMachine;
    private bool isFacingRight = true;

    [Header("Movement")]
    public float moveSpeed = 5f;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        input = new PlayerInputSet();
        stateMachine = new StateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
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
        stateMachine.Update();
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
}
