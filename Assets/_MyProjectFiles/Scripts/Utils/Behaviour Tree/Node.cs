using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public abstract class Node
{
	public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    protected NodeState state;
    public Node parent;
    protected List<Node> children = new List<Node>();
    private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

    #region Base Methods
    // Constructors
    public Node()
    {
        parent = null;
    }
    public Node(List<Node> children)
    {
        foreach(Node child in children)
        {
            _Attach(child);
        }
    }
    private void _Attach(Node node)
    {
        node.parent = this;
        children.Add(node);
    }

    public abstract NodeState Evaluate();
    #endregion

    #region Data Manipulation Methods
    public void SetData(string key, object value)
    {
        _dataContext[key] = value;
    }

    public object GetData(string key)
    {
        object value = null;
        
        // if data found in current node
        if(_dataContext.TryGetValue(key, out value))
        {
            return value;
        }

        // Traverse tree to get value
        Node node = parent;
        while(node != null)
        {
            value = node.GetData(key);
            if(value != null)
            {
                return value;
            }

            node = node.parent;
        }

        // if value can't be found
        return null;
    }

    public bool ClearData(string key)
    {   
        // if key found in current node
        if(_dataContext.ContainsKey(key))
        {
            _dataContext.Remove(key);
            return true;
        }

        // Traverse tree to get key pair
        Node node = parent;
        while(node != null)
        {
            bool cleared= node.ClearData(key);
            if(cleared)
            {
                return true;
            }

            node = node.parent;
        }

        // if value can't be found
        return false;
    }
    #endregion
    
}
