using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
  private float attackVelocityTimer = 0f;
  public PlayerBasicAttackState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Enter()
  {
    base.Enter();
    attackVelocityTimer = m_Player.attackVelocityDuration;
  }

  public override void Update()
  {
    base.Update();
    HandleAttackVelocity();

    if (triggerCalled)
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }
  }

  private void HandleAttackVelocity()
  {
    attackVelocityTimer -= Time.deltaTime;

    if (attackVelocityTimer < 0)
      m_Player.SetVelocity(0, m_Rigidbody.linearVelocityY);
  }
}