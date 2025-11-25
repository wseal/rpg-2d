using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
  private float attackVelocityTimer = 0.2f;
  private float lastTimeAttacked = 0f;

  private bool comboAttackQueued;
  private int attackDir;
  private int comboIndex = 1;
  private int ComboLimit = 3;
  private const int FirstComboIndex = 1;


  public PlayerBasicAttackState(Player player, StateMachine machine, string name) : base(player, machine, name)
  {
    if (ComboLimit != m_Player.attackVelocity.Length)
    {
      ComboLimit = m_Player.attackVelocity.Length;
    }
  }

  public override void Enter()
  {
    base.Enter();
    comboAttackQueued = false;
    ResetComboIndexIfNeeded();

    if (m_Player.movementInput.x != 0)
    {
      attackDir = m_Player.movementInput.x > 0 ? 1 : -1;
    }
    else
    {
      attackDir = m_Player.facingDir;
    }

    m_Animator.SetInteger("BasicAttackIndex", comboIndex);
    ApplyAttackVelocity(); // all this setting maybe override by pre state update code.
  }

  public override void Update()
  {
    base.Update();
    HandleAttackVelocity();

    if (m_Input.Player.Attack.WasPressedThisFrame())
      QueueNextAttack();

    if (triggerCalled)
      HandleStateExit();
  }

  public override void Exit()
  {
    base.Exit();
    comboIndex += 1;
    lastTimeAttacked = Time.time; // remember time when we attacked
  }

  private void HandleStateExit()
  {
    if (comboAttackQueued)
    {
      // m_StateMachine.ChangeState(m_Player.basicAttackState);
      m_Animator.SetBool(m_AnimationBoolName, false);
      m_Player.EnterAttackStateWithDelay();
    }
    else
    {
      m_StateMachine.ChangeState(m_Player.idleState);
    }

  }

  private void QueueNextAttack()
  {
    if (comboIndex < ComboLimit)
    {
      comboAttackQueued = true;
    }
  }

  private void HandleAttackVelocity()
  {
    attackVelocityTimer -= Time.deltaTime;
    if (attackVelocityTimer < 0)
      m_Player.SetVelocity(0, m_Rigidbody.linearVelocityY);
  }

  void ApplyAttackVelocity()
  {
    attackVelocityTimer = m_Player.attackVelocityDuration;

    var attackVel = m_Player.attackVelocity[comboIndex - 1];
    m_Player.SetVelocity(attackDir * attackVel.x, attackVel.y);
  }

  void ResetComboIndexIfNeeded()
  {
    // if time we attacked was  long ago, reset combo index
    if (comboIndex > ComboLimit || Time.time > lastTimeAttacked + m_Player.comboResetTime)
    {
      comboIndex = FirstComboIndex;
    }
  }
}