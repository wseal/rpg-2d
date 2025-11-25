using UnityEngine;

public class EnemySate : EntityState
{
    protected Enemy enemy;
    public EnemySate(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        anim = enemy.anim;
    }
}
