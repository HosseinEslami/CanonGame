using UnityEngine;

public class RangeNode : Node
{
    private readonly float _range;
    private readonly Transform _origin, _target;

    public RangeNode(float range, Transform origin, Transform target)
    {
        _range = range;
        _origin = origin;
        _target = target;
    }

    public override NodeState Evaluate(object obj)
    {

        var distance = Mathf.Abs(_target.position.x - _origin.transform.position.x);
        
        // Debug.Log("dist = " + distance);

        return distance <= _range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}