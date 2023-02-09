using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------
public abstract class Tree : MonoBehaviour
{
	private Node _root = null;

	#region Base Methods
	protected virtual void Start()
	{
		_root = SetUpTree();
	}

	protected virtual void Update()
	{
		if(_root != null)
		{
			_root.Evaluate();
		}
	}

	protected abstract Node SetUpTree();
	#endregion
}
