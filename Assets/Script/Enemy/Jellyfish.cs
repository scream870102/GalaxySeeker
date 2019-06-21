using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>Enemy:Jellyfish</summary>
public class Jellyfish : Enemy {
    // stroe all values which child component need
    [SerializeField]
    JellyfishProps props;
    // define how jellyfish move
    JellyfishMovement movement = null;
    // define how jellyfish attack
    JellyfishAttack attack = null;

    // spawn all components
    protected override void Init ( ) {
        base.Init ( );
        movement = new JellyfishMovement (this, props.Speed, props.traceSpeed, props.Range, props.DetectAreaRange);
        attack = new JellyfishAttack (this, props.AttackDetectArea, stats.damage.Value, props.AttackCD);
    }

    // when jellyFish dead disable it
    protected override void Dead ( ) {
        Debug.Log ("who am I? " + name + " Dead");
        this.IsEnable = false;
    }

    /// <summary>define all props that jellyfish need</summary>
    [System.Serializable]
    struct JellyfishProps {
        [Header ("Move Property")]
        /// <summary>move velocity</summary>
        public float Speed;
        /// <summary>velocity when tracing target</summary>
        public float traceSpeed;
        /// <summary>move Range</summary>
        /// <remarks>jelly fish will always stay in the ellipse and x represent x radius and so on y</remarks>
        public Vector2 Range;
        /// <summary>define the radius of circle which jellyfish can detect player</summary>
        public float DetectAreaRange;
        [Header ("Attack Property")]
        /// <summary>define radius of circle when player enter it Jellyfish will launch attack</summary>
        public float AttackDetectArea;
        /// <summary>interval time between two attack</summary>
        public float AttackCD;

    }

}
