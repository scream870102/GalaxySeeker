using System.Collections.Generic;

using Eccentric.UnityUtils.Attack;

using UnityEngine;
[System.Serializable]
/// <summary>define how CannibalFlower attack target</summary>
public class KingCannibalFlowerAttack : CharacterComponent {
    //-----ref
    List<Collider2D> vines = new List<Collider2D> ( );
    AttackPair vine;
    AttackPair vineScratch;
    AttackPair bite;
    LayerMask targetLayer;
    float normalProbability;
    float specialProbability;
    //------field
    Character target = null;
    Transform targetTransform = null;
    KingCannibalFlower kcf = null;
    AttackPair currentAttack = null;
    List<AttackPair> specialAttacks = new List<AttackPair> ( );
    List<AttackPair> attacks = new List<AttackPair> ( );
    List<AttackPair> normalAttacks = new List<AttackPair> ( );
    public KingCannibalFlowerAttack (Enemy parent, LayerMask targetLayer, float normalProbability, float specialProbability, AttackValue vineValue, AttackValue vineScratchValue, AttackValueRadius biteValue, List<Collider2D> vines) : base (parent) {
        kcf = (KingCannibalFlower) Parent;
        this.targetLayer = targetLayer;
        this.vines.AddRange (vines);
        this.normalProbability = normalProbability;
        this.specialProbability = specialProbability;
        attacks.Add (this.vine = new AttackPair (vineValue, new BasicAttack (vineValue.Damage, vineValue.CD, VineAttacking)));
        attacks.Add (this.vineScratch = new AttackPair (vineScratchValue, new BasicAttack (vineScratchValue.Damage, vineScratchValue.CD, VineScratchAttacking)));
        attacks.Add (this.bite = new AttackPair (biteValue, new CircleAreaAttack (biteValue.CD, biteValue.DetectRadius, targetLayer)));
        specialAttacks.Add (vineScratch);
        specialAttacks.Add (vine);
        kcf.OnAnimationFinished += AnimFinished;
        Eccentric.UnityUtils.Physics2D.OverlapCircle (Parent.tf.position, Mathf.Infinity, targetLayer, ref targetTransform);
        if (targetTransform)
            target = targetTransform.GetComponent<Character> ( );
    }

    protected override void FixedTick ( ) {
        foreach (AttackPair attack in attacks)
            attack.action.UpdateState (Parent.tf.position);
        //沒有攻擊中的 選擇一個攻擊
        if (currentAttack == null) {
            //選擇普通攻擊或是特殊攻擊
            bool bUseSpecial = Eccentric.Math.ChosenDueToProbability (specialProbability, normalProbability);
            //特殊攻擊
            if (bUseSpecial) {
                List<AttackPair> actionToUse = new List<AttackPair> ( );
                foreach (AttackPair attack in specialAttacks)
                    if (attack.action.IsCanAttack) actionToUse.Add (attack);
                if (actionToUse.Count == 1) currentAttack = actionToUse [0];
                else if (actionToUse.Count == 0) return;
                else currentAttack = actionToUse [Eccentric.Math.ChosenDueToProbability (actionToUse.Count)];
            }
            //普通攻擊
            else {
                currentAttack = bite;
            }
            if (currentAttack.action.IsCanAttack) {
                currentAttack.action.Attack ( );
                kcf.Anim.Play (currentAttack.value.Clip.name);
            }
            else currentAttack = null;

        }
    }

    void VineAttacking ( ) {
        foreach (Collider2D vineCol in vines) {
            List<Collider2D> cols = new List<Collider2D> ( );
            ContactFilter2D filter = new ContactFilter2D ( );
            filter.SetLayerMask (targetLayer);
            vineCol.OverlapCollider (filter, cols);
            foreach (Collider2D col in cols)
                if (target) this.vine.action.CauseDamage (target, this.vine.value.Damage);
        }
    }
    void VineScratchAttacking ( ) {
        foreach (Collider2D vineCol in vines) {
            List<Collider2D> cols = new List<Collider2D> ( );
            ContactFilter2D filter = new ContactFilter2D ( );
            filter.SetLayerMask (targetLayer);
            vineCol.OverlapCollider (filter, cols);
            foreach (Collider2D col in cols)
                if (target) this.vineScratch.action.CauseDamage (target, this.vineScratch.value.Damage);
        }
    }

    void AnimFinished (AnimationClip anim) {
        if (anim.name == vine.value.Clip.name) {
            vine.action.AttackFinished ( );
        }
        else if (anim.name == vineScratch.value.Clip.name) {
            vineScratch.action.AttackFinished ( );
        }
        else if (anim.name == bite.value.Clip.name) {
            bite.action.CauseDamage (target, this.bite.value.Damage);
            bite.action.AttackFinished ( );
        }
        else return;
        kcf.Anim.Play (kcf.Anim.clip.name);
        currentAttack = null;
    }

}
