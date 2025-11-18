
using UnityEngine;

public abstract class EntityState
{
    protected Player m_Player;
    protected StateMachine m_StateMachine;
    protected string m_AnimationBoolName;

    private Animator m_Animator;

    public EntityState(Player player, StateMachine machine, string animBoolName)
    {
        m_Player = player;
        m_StateMachine = machine;
        m_AnimationBoolName = animBoolName;
        m_Animator = player.animator;
    }

    // State will be change into , Eneter will be call
    public virtual void Enter()
    {
        // Debug.Log($"Enter:{m_StateName}");
        m_Animator.SetBool(m_AnimationBoolName, true);
    }

    // run logic
    public virtual void Update() { }

    // will be call, when state exit
    public virtual void Exit()
    {
        m_Animator.SetBool(m_AnimationBoolName, false);
    }
}
