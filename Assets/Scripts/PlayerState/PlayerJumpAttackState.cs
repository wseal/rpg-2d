public class PlayerJumpAttackState : EntityState
{

  private bool toucheGround = false;
  public PlayerJumpAttackState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
  }

  public override void Enter()
  {
    base.Enter();
    toucheGround = false;

    m_Player.SetVelocity(m_Player.jumpAttackVelocity.x * m_Player.facingDir, m_Player.jumpAttackVelocity.y);
  }

  public override void Update()
  {
    base.Update();
    if (m_Player.groundDetected && !toucheGround)
    {
      toucheGround = true;
      m_Animator.SetTrigger("JumpAttackTrigger");
      m_Player.SetVelocity(0, m_Rigidbody.linearVelocityY);
    }

    if (m_Player.groundDetected && triggerCalled)
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }
  }
}