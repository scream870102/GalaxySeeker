using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private Player parent = null;
    public Player Parent { set { if (parent == null) parent = value; } get{return parent;}}
    // Update is called once per frame
    protected virtual void Update()   {
        if (parent == null)
            return;
    }
    protected virtual void FixedUpdate() {
        if (parent == null)
            return;
    }
}
