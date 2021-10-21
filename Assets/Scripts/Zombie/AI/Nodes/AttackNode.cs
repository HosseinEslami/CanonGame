using System;
using UnityEngine;


public class AttackNode : Node
{
    private readonly Zombie _zombie;
    private readonly ZombieAI _ai;
    private bool _attacked;

    public AttackNode(Zombie zombie)
    {
        _zombie = zombie;
    }

    public override NodeState Evaluate(object obj)
    {
        //return NodeState.FAILURE;
        if (!_attacked) 
        {
            // Debug.Log("Attack");
            _zombie.Attack();
            _attacked = true;
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}