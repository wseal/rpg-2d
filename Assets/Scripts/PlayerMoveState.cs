using UnityEngine;

public class PlayerMoveState : EntityState
{
  public PlayerMoveState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Update()
  {
    base.Update();

    // player.rb.velocity = new Vector2(m_Player.movementInput.x * player.moveSpeed, player.rb.velocity.y);
    if (m_Player.movementInput.x == 0)
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }
  }
}
