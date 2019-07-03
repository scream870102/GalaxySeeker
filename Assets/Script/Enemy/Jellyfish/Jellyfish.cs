using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>Enemy:Jellyfish</summary>
public class Jellyfish : Enemy {
    // store all values which child component need
    [SerializeField]
    JellyfishProps props;
    // define how jellyfish move
    JellyfishMovement movement = null;
    // define how jellyfish attack
    JellyfishAttack attack = null;

    // spawn all components
    protected override void Init ( ) {
        base.Init ( );
        movement = new JellyfishMovement (this, props.Speed, props.TraceSpeed, props.Range, props.DetectAreaRadius, props.TargetLayer);
        attack = new JellyfishAttack (this, props.DetectRadius, stats.damage.Value, props.Cooldown, props.TargetLayer);
    }

    // when jellyFish dead disable it
    protected override void Dead ( ) {
        Debug.Log ("who am I? " + name + " Dead");
        this.IsEnable = false;
    }

    /// <summary>define all props that jellyfish need</summary>
    [System.Serializable]
    struct JellyfishProps {
        [Header ("Common Property")]
        /// <summary>what layer should jellyfish react with</summary>
        public LayerMask TargetLayer;
        [Header ("Move Property")]
        /// <summary>move velocity</summary>
        public float Speed;
        /// <summary>velocity when tracing target</summary>
        public float TraceSpeed;
        /// <summary>move Range</summary>
        /// <remarks>jelly fish will always stay in the ellipse and x represent x radius and so on y</remarks>
        public Vector2 Range;
        /// <summary>define the radius of circle which jellyfish can detect player</summary>
        public float DetectAreaRadius;
        [Header ("Attack Property")]
        /// <summary>define radius of circle when player enter it Jellyfish will launch attack</summary>
        public float DetectRadius;
        /// <summary>interval time between two attack</summary>
        public float Cooldown;

    }

}
