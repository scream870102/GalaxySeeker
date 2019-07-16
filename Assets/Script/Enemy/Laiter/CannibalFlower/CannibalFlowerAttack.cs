using Eccentric.UnityUtils.Attack;

using UnityEngine;
[System.Serializable]
/// <summary>define how CannibalFlower attack target</summary>
public class CannibalFlowerAttack : CharacterComponent {
    //-----ref
    AttackValueRadius needleValue;
    AttackValueRadius biteValue;
    LayerMask targetLayer;

    //------field
    CircleAreaAttack needleAction = null;
    CircleAreaAttack biteAction = null;

    public CannibalFlowerAttack (Enemy parent, AttackValueRadius needle, AttackValueRadius bite, LayerMask targetLayer) : base (parent) {
        this.needleValue = needle;
        this.biteValue = bite;
        this.targetLayer = targetLayer;
        needleAction = new CircleAreaAttack (needle.CD, needle.DetectRadius, targetLayer);
        biteAction = new CircleAreaAttack (bite.DetectRadius, bite.CD, targetLayer);
    }

    protected override void FixedTick ( ) {
        needleAction.UpdateState (Parent.tf.position);
        biteAction.UpdateState (Parent.tf.position);
        //if player in the close range use bite attack
        if (biteAction.IsCanAttack)
            biteAction.Attack ( );
        //if player in the far range and outof bite range use needle attack
        else if (needleAction.IsCanAttack && needleAction.DistanceBetweenTarget > biteValue.DetectRadius)
            needleAction.Attack ( );
    }

}
