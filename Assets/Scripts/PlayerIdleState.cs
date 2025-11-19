using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
  public PlayerIdleState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Enter()
  {
    base.Enter();
    m_Player.SetVelocity(0, m_Rigidbody.linearVelocityY);
  }

  public override void Update()
  {
    base.Update();

    if (m_Player.movementInput.x == m_Player.facingDir && m_Player.wallDetected)
    {
      return;
    }

    if (m_Player.movementInput.x != 0)
    {
      m_StateMachine.ChangeState(m_Player.moveState);
    }
  }
}
