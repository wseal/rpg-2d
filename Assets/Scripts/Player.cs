using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Vector2 movementInput;

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private PlayerInputSet input;
    private StateMachine stateMachine;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        input = new PlayerInputSet();
        stateMachine = new StateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");

        stateMachine.Initialize(idleState);
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

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        stateMachine.Update();
    }
}
