using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class SequenceNode : Node
{
    public SequenceNode() : base() { }
    public SequenceNode(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildIsRunning = false;

        foreach (Node node in children)
		{
			switch(node.Evaluate())
			{
				case NodeState.FAILURE:
					state = NodeState.FAILURE;
					return state;
				case NodeState.SUCCESS:
					continue;
				case NodeState.RUNNING:
					anyChildIsRunning = true;
					continue;
				default:
					state = NodeState.SUCCESS;
					return state;
			}
		}

		state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
		return state;
    }
}
