using UnityEngine;

public class MoveNode : Node
{
    private readonly Zombie _zombie;

    public MoveNode(Zombie zombie)
    {
        _zombie = zombie;
    }

    public override NodeState Evaluate(object startcourotain)
    {
        //Move
        //Debug.Log("Moving");
        return NodeState.RUNNING;
    }

}
