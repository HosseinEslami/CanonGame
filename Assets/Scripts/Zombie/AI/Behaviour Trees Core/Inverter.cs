public class Inverter : Node
{
	protected Node node;

	public Inverter(Node node)
	{
		this.node = node;
	}
	public override NodeState Evaluate(object startcourotain)
	{
		switch (node.Evaluate(startcourotain))
		{
			case NodeState.RUNNING:
				_nodeState = NodeState.RUNNING;
				break;
			case NodeState.SUCCESS:
				_nodeState = NodeState.FAILURE;
				break;
			case NodeState.FAILURE:
				_nodeState = NodeState.SUCCESS;
				break;
			default:
				break;
		}
		return _nodeState;
	}
}
