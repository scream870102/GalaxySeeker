using Eccentric.UnityModel.Attack;

using UnityEngine;
[System.Serializable]
/// <summary>define how CannibalFlower attack target</summary>
public class CannibalFlowerAttack : CharacterComponent {
    //-----ref
    AttackValue needleValue;
    AttackValue biteValue;
    LayerMask targetLayer;

    //------field
    CircleAreaAttack needleAction = null;
    CircleAreaAttack biteAction = null;

    public CannibalFlowerAttack (Enemy parent, AttackValue needle, AttackValue bite, LayerMask targetLayer) : base (parent) {
        this.needleValue = needle;
        this.biteValue = bite;
        this.targetLayer = targetLayer;
        needleAction = new CircleAreaAttack (needle.DetectRadius, needle.CD, targetLayer);
        biteAction = new CircleAreaAttack (bite.DetectRadius, bite.CD, targetLayer);
    }

    protected override void FixedTick ( ) {
        needleAction.UpdateState (Parent.tf.position);
        biteAction.UpdateState (Parent.tf.position);
        //if player in the close range use bite attack
        if (biteAction.IsCanAttack)
            biteAction.Attack (biteValue.Damage);
        //if player in the far range and outof bite range use needle attack
        else if (needleAction.IsCanAttack && needleAction.DistanceBetweenTarget > biteValue.DetectRadius)
            needleAction.Attack (needleValue.Damage);
    }

}
