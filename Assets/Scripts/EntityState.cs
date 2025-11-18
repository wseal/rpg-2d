
using UnityEngine;

public abstract class EntityState
{
    protected Player m_Player;
    protected StateMachine m_StateMachine;
    protected string m_AnimationBoolName;

    protected Animator m_Animator;
    protected Rigidbody2D m_Rigidbody;
    protected PlayerInputSet m_Input;

    protected float m_StateTimer;
    protected bool triggerCalled = false;

    public EntityState(Player player, StateMachine machine, string animBoolName)
    {
        m_Player = player;
        m_StateMachine = machine;
        m_AnimationBoolName = animBoolName;
        m_Animator = player.animator;
        m_Rigidbody = player.rb;
        m_Input = player.input;
    }

    // State will be change into , Eneter will be call
    public virtual void Enter()
    {
        // Debug.Log($"Enter:{m_StateName}");
        m_Animator.SetBool(m_AnimationBoolName, true);
        triggerCalled = false;
    }

    // run logic
    public virtual void Update()
    {
        m_StateTimer -= Time.deltaTime;
        m_Animator.SetFloat("YVelocityY", m_Rigidbody.linearVelocityY);

        if (m_Input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            m_StateMachine.ChangeState(m_Player.dashState);
        }
    }

    // will be call, when state exit
    public virtual void Exit()
    {
        m_Animator.SetBool(m_AnimationBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    bool CanDash()
    {
        if (m_Player.wallDetected)
        {
            return false;
        }

        if (m_Player.dashState == m_StateMachine.currentState)
        {
            return false;
        }

        return true;
    }
}
