public class PlayerAiredState : EntityState
{
  public PlayerAiredState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Update()
  {
    base.Update();

    if (m_Player.movementInput.x != 0)
    {
      m_Player.SetVelocity(m_Player.movementInput.x * m_Player.moveSpeed * m_Player.inAirMoveMultiplier, m_Rigidbody.linearVelocityY);
    }

    if (m_Input.Player.Attack.WasPressedThisFrame())
    {
      m_StateMachine.ChangeState(m_Player.jumpAttackState);
    }
  }
}