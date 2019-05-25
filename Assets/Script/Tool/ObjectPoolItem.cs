using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectPoolItem : MonoBehaviour {
    public ObjectPool pool;
    // Update is called once per frame
    void Update ( ) {
        if (pool == null)
            return;
        Tick ( );
    }
    void FixedUpdate ( ) {
        if (pool == null)
            return;
        FixedTick ( );
    }

    protected virtual void Tick ( ) { }
    protected virtual void FixedTick ( ) { }

    protected void Recycle ( ) {
        if (pool == null)
            return;
        pool.RecycleObject (this);
    }

    public virtual void Init ( ) { }
}
