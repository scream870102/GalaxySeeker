using UnityEngine;

///<summary>parent class of all player component
public class PlayerComponent : MonoBehaviour {
    //which player does this component belongs to
    Player parent = null;
    /// <summary>property of parent</summary>
    public Player Parent { set { if (parent == null) parent = value; } get { return parent; } }
    void Update ( ) {
        if (Parent == null)
            return;
        Tick ( );
    }
    void FixedUpdate ( ) {
        if (Parent == null)
            return;
        FixedTick ( );
    }

    protected virtual void Tick ( ) { }
    protected virtual void FixedTick ( ) { }

}
