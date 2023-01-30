using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class SelectorNode : Node
{
    public SelectorNode() : base() { }
    public SelectorNode(List<Node> children) : base(children) { }
    public override NodeState Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
					continue;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                default:
					continue;
            }
        }

		// if no success
        state = NodeState.FAILURE;
        return state;
    }
}
