public class PlayerWallJumpState : EntityState
{
  public PlayerWallJumpState(Player player, StateMachine machine, string animBoolName) : base(player, machine, animBoolName)
  {
  }

  override public void Enter()
  {
    base.Enter();
    m_Player.SetVelocity(m_Player.wallJumpForce.x * -m_Player.facingDir, m_Player.wallJumpForce.y);
  }


  override public void Update()
  {
    base.Update();

    if (m_Rigidbody.linearVelocityY < 0)
    {
      m_StateMachine.ChangeState(m_Player.fallState);
    }

    if (m_Player.wallDetected)
    {
      m_StateMachine.ChangeState(m_Player.wallSlideState);
    }
  }
}