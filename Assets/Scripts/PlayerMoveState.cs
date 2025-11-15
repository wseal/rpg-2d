using UnityEngine;

public class PlayerMoveState : EntityState
{
  public PlayerMoveState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Update()
  {
    base.Update();

    if (Input.GetKey(KeyCode.G))
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }
  }
}
