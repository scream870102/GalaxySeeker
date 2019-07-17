using System.Collections.Generic;

using Eccentric.UnityUtils;
using Eccentric.UnityUtils.Attack;

using UnityEngine;
[System.Serializable]
/// <summary>define how CannibalFlower attack target</summary>
public class CannibalFlowerAttack : CharacterComponent {
    //-----ref
    //AttackValueRadius needleValue;
    //AttackValueRadius biteValue;
    LayerMask targetLayer;

    //------field
    AttackSet needle;
    AttackSet bite;
    AttackSet currentAttack = null;
    List<AttackSet> attacks = new List<AttackSet> ( );
    CannibalFlower cf;
    List<Collider2D> needles = new List<Collider2D> ( );
    bool bFacingRight = true;
    Transform target = null;

    public CannibalFlowerAttack (Enemy parent, AttackValueRadius needle, AttackValueRadius bite, LayerMask targetLayer, List<Collider2D> needles) : base (parent) {
        this.targetLayer = targetLayer;
        this.needles.AddRange (needles);
        attacks.Add (this.needle = new AttackSet (needle, new CircleAreaAttack (needle.CD, needle.DetectRadius, targetLayer, this.NeedleAttacking)));
        attacks.Add (this.bite = new AttackSet (bite, new CircleAreaAttack (bite.CD, bite.DetectRadius, targetLayer)));
        cf = (CannibalFlower) Parent;
        cf.OnAnimationFinished += this.AnimFinished;
        Eccentric.UnityUtils.Physics2D.OverlapCircle (Parent.tf.position, Mathf.Infinity, targetLayer, ref target);
    }

    protected override void FixedTick ( ) {
        if (target)
            bFacingRight = Eccentric.UnityUtils.Physics2D.IsRight (Parent.tf.position, target.position);
        foreach (AttackSet attack in attacks)
            attack.action.UpdateState (Parent.tf.position);
        //Change render direction
        if (currentAttack == null) {
            Render.ChangeDirection (bFacingRight, Parent.transform);
            //if player in the close range use bite attack
            if (bite.action.IsCanAttack)
                currentAttack = bite;
            //if player in the far range and outof bite range use needle attack
            else if (needle.action.IsCanAttack && (needle.action as CircleAreaAttack).DistanceBetweenTarget > (bite.value as AttackValueRadius).DetectRadius)
                currentAttack = needle;
        }
        if (currentAttack != null) {
            cf.Anim.Play (currentAttack.value.Clip.name);
            currentAttack.action.Attack ( );
        }
    }

    void AnimFinished (AnimationClip anim) {
        if (currentAttack == bite)
            (currentAttack.action as CircleAreaAttack).CauseDamage (currentAttack.value.Damage);
        if (anim.name == currentAttack.value.Clip.name) {
            currentAttack.action.AttackFinished ( );
            cf.Anim.Play (cf.Anim.clip.name);
            currentAttack = null;
        }
    }

    void NeedleAttacking ( ) {
        //get all collider which collide with vine in all vines
        foreach (Collider2D needle in this.needles) {
            List<Collider2D> cols = new List<Collider2D> ( );
            ContactFilter2D filter = new ContactFilter2D ( );
            filter.SetLayerMask (targetLayer);
            needle.OverlapCollider (filter, cols);
            foreach (Collider2D col in cols)
                (this.needle.action as CircleAreaAttack).CauseDamage (this.needle.value.Damage);
        }
    }

}
