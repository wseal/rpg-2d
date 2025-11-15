
using UnityEngine;

public abstract class EntityState
{
    protected Player m_Player;
    protected StateMachine m_StateMachine;
    protected string m_StateName;

    public EntityState(Player player, StateMachine machine, string name)
    {
        m_Player = player;
        m_StateMachine = machine;
        m_StateName = name;
    }

    // State will be change into , Eneter will be call
    public virtual void Enter()
    {
        Debug.Log($"Enter:{m_StateName}");
    }

    // run logic
    public virtual void Update() { }

    // will be call, when state exit
    public virtual void Exit()
    {
        Debug.Log($"Leave:{m_StateName}");
    }
}
