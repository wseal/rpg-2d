public class PlayerJumpState : PlayerAiredState
{
  public PlayerJumpState(Player player, StateMachine machine, string animBoolName) : base(player, machine, animBoolName)
  {
  }

  override public void Enter()
  {
    base.Enter();
    m_Player.SetVelocity(m_Rigidbody.linearVelocityX, m_Player.jumpForce);
  }

  public override void Update()
  {
    base.Update();

    if (m_Rigidbody.linearVelocityY < 0 && m_StateMachine.currentState != m_Player.jumpAttackState)
    {
      m_StateMachine.ChangeState(m_Player.fallState);
    }
  }
}