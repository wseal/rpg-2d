public class PlayerDashState : EntityState
{
  private float originGravityScale = 0f;
  private int dashDir;
  public PlayerDashState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }


  public override void Enter()
  {
    base.Enter();

    m_StateTimer = m_Player.dashDuration;
    dashDir = m_Player.facingDir;
    originGravityScale = m_Rigidbody.gravityScale;
    m_Rigidbody.gravityScale = 0;
  }

  public override void Update()
  {
    base.Update();

    m_Player.SetVelocity(dashDir * m_Player.dashSpeed, 0);

    if (m_StateTimer <= 0)
    {
      if (m_Player.groundDetected)
      {
        m_StateMachine.ChangeState(m_Player.idleState);
      }
      else
      {
        m_StateMachine.ChangeState(m_Player.fallState);
      }
    }
  }

  public override void Exit()
  {
    base.Exit();
    m_Player.SetVelocity(0, 0);
    m_Rigidbody.gravityScale = originGravityScale;
  }

  private void CancelDashIfNeeded()
  {
    // if (m_Player.input.Player.Jump.WasPressedThisFrame())
    // {
    //   m_StateMachine.ChangeState(m_Player.fallState);
    // }
    if (m_Player.wallDetected)
    {
      if (m_Player.groundDetected)
      {
        m_StateMachine.ChangeState(m_Player.idleState);
      }
      else
      {
        m_StateMachine.ChangeState(m_Player.wallSlideState);
      }
    }
  }
}