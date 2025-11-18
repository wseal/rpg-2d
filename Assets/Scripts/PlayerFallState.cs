public class PlayerFallState : PlayerAiredState
{
  public PlayerFallState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Update()
  {
    base.Update();

    if (m_Player.groundDetected)
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }

    if (m_Player.wallDetected)
    {
      m_StateMachine.ChangeState(m_Player.wallSlideState);
    }
  }
}