

public class PlayerWallSlideState : EntityState
{
  public PlayerWallSlideState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Enter()
  {
    base.Enter();
    // m_Player.SetVelocity(0, m_Rigidbody.linearVelocityY);
  }

  public override void Update()
  {
    base.Update();

    HandleWallSlide();
    // if (!m_Player.wallDetected)
    // {
    //   m_StateMachine.ChangeState(m_Player.fallState);
    // }

    if (!m_Player.wallDetected)
    {
      m_StateMachine.ChangeState(m_Player.fallState);
    }

    if (m_Player.groundDetected)
    {
      m_StateMachine.ChangeState(m_Player.idleState);
      m_Player.Flip();
    }
  }

  void HandleWallSlide()
  {
    if (m_Player.movementInput.y < 0)
    {
      m_Player.SetVelocity(m_Player.movementInput.x, m_Rigidbody.linearVelocityY);
    }
    else
    {
      m_Player.SetVelocity(m_Player.movementInput.x, m_Rigidbody.linearVelocityY * m_Player.wallSlideSlowMultiplier);
    }
  }
}