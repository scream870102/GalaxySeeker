using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
/// <summary>define how CannibalFlower attack target</summary>
public class CannibalFlowerAttack : CharacterComponent {
    //-----ref
    AttackValue needleValue;
    AttackValue biteValue;
    LayerMask targetLayer;

    //------field
    Eccentric.Attack.CircleAreaAttack needleAction = null;
    Eccentric.Attack.CircleAreaAttack biteAction = null;

    public CannibalFlowerAttack (Enemy parent, AttackValue needle, AttackValue bite, LayerMask targetLayer) : base (parent) {
        this.needleValue = needle;
        this.biteValue = bite;
        this.targetLayer = targetLayer;
        needleAction = new Eccentric.Attack.CircleAreaAttack (needle.DetectRadius, needle.CD, targetLayer);
        biteAction = new Eccentric.Attack.CircleAreaAttack (bite.DetectRadius, bite.CD, targetLayer);
    }

    protected override void FixedTick ( ) {
        needleAction.UpdateState (Parent.tf.position);
        biteAction.UpdateState (Parent.tf.position);
        // if (biteAction.IsCanAttack)
        //     biteAction.Attack (biteValue.Damage, biteAction.TargetCharacter.Stats);
        // else if (needleAction.IsCanAttack)
        //     needleAction.Attack (needleValue.Damage, biteAction.TargetCharacter.Stats);
    }

}
