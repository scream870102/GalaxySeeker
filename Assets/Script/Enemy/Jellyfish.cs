using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>Enemy:Jellyfish</summary>
public class Jellyfish : Enemy {
    // stroe all values which child component need
    [Header ("Move Property")]
    [SerializeField]
    JellyfishProps props;
    // define how jellyfish move
    JellyfishMovement movement = null;

    // spawn all components
    protected override void Init ( ) {
        base.Init ( );
        movement = new JellyfishMovement (this, props.Speed, props.Range);
    }

    // when jellyFish dead disable it
    protected override void Dead ( ) {
        Debug.Log ("who am I? " + name + " Dead");
        this.IsEnable = false;
    }

    /// <summary>define all props that jellyfish need</summary>
    [System.Serializable]
    struct JellyfishProps {
        /// <summary>move velocity</summary>
        public float Speed;
        /// <summary>move Range</summary>
        /// <remarks>jelly fish will always stay in the ellipse and x represent x radius and so on y</remarks>
        public Vector2 Range;
    }

}
