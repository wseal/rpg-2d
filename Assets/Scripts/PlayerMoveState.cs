using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
  public PlayerMoveState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Update()
  {
    base.Update();

    // player.rb.velocity = new Vector2(m_Player.movementInput.x * player.moveSpeed, player.rb.velocity.y);
    if (m_Player.movementInput.x == 0 || m_Player.wallDetected)
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }

    m_Player.SetVelocity(m_Player.movementInput.x * m_Player.moveSpeed, m_Rigidbody.linearVelocityY);
  }
}
