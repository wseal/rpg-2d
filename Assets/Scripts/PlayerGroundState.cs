public class PlayerGroundState : EntityState
{
  public PlayerGroundState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Update()
  {
    base.Update();
    if (m_Rigidbody.linearVelocityY < 0 && !m_Player.groundDetected)
    {
      m_StateMachine.ChangeState(m_Player.fallState);
    }

    if (m_Player.input.Player.Jump.WasPressedThisFrame())
    {
      m_StateMachine.ChangeState(m_Player.jumpState);
    }

    if (m_Input.Player.Attack.WasPressedThisFrame())
    {
      m_StateMachine.ChangeState(m_Player.basicAttackState);
    }
  }
}