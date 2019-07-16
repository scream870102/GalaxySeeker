﻿using Eccentric.UnityUtils.Attack;

using UnityEngine;
/// <summary>Enemy:CannibalFlower</summary>
public class CannibalFlower : Enemy {
    //---------ref
    //store all values which child component need
    [SerializeField]
    CannibalFlowerProps props;

    //--------field
    //define how cannibalFLower attack
    CannibalFlowerAttack attack = null;
    protected override void Init ( ) {
        base.Init ( );
        attack = new CannibalFlowerAttack (this, props.Needle, props.Bite, props.TargetLayer);
    }

    protected override void Dead ( ) {
        Debug.Log ("My name is " + name + " I am dead");
        this.IsEnable = false;
    }

    [System.Serializable]
    struct CannibalFlowerProps {
        [Header ("Common Property")]
        public LayerMask TargetLayer;
        [Header ("Attack Property")]
        /// <summary>needle is for far attack</summary>
        public AttackValueRadius Needle;
        /// <summary>bite is for close attack</summary>
        public AttackValueRadius Bite;
    }

}