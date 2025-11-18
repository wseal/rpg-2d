public class PlayerFallState : PlayerAiredState
{
  public PlayerFallState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Update()
  {
    base.Update();

    if (m_Player.isGrounded)
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }
  }
}