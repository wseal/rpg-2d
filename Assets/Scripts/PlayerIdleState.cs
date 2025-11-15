using UnityEngine;

public class PlayerIdleState : EntityState
{
  public PlayerIdleState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }


  public override void Update()
  {
    base.Update();

    if (Input.GetKey(KeyCode.F))
    {
      m_StateMachine.ChangeState(m_Player.moveState);
    }
  }
}
