using System.Collections;
using System.Collections.Generic;

using UnityEngine;

///<summary>parent class of all player component
public class PlayerComponent : MonoBehaviour {
    //which player does this component belongs to
    private Player parent = null;
    /// <summary>property of parent</summary>
    public Player Parent { set { if (parent == null) parent = value; } get { return parent; } }

    //if parent doesn't exist do nothing
    protected virtual void Update ( ) {
        if (parent == null)
            return;
    }

    //if parent doesn't exist do nothing
    protected virtual void FixedUpdate ( ) {
        if (parent == null)
            return;
    }
}
