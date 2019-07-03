using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>Enemy:CannibalFlower</summary>
public class CannibalFlower : Enemy {
    //store all values which child component need
    [SerializeField]
    CannibalFlowerProps props;
    //define how cannibalFLower attack
    CannibalFlowerAttack attack = null;
    protected override void Init ( ) {
        base.Init ( );
        attack = new CannibalFlowerAttack (this, props.DetectRadius, props.Cooldown, stats.damage.Value, props.TargetLayer);
    }

    protected override void Dead ( ) {
        Debug.Log ("My name is " + name + " I am dead");
        this.IsEnable = false;
    }

    [System.Serializable]
    struct CannibalFlowerProps {
        [Header ("Common Property")]
        /// <summary>what layer should cannibalFlower react with</summary>
        public LayerMask TargetLayer;
        [Header ("Attack Property")]
        /// <summary>define radius of circle when player enter it CannibalFlower will attack it</summary>
        public float DetectRadius;
        /// <summary>define the time between two attack actions</summary>
        public float Cooldown;

    }

}
