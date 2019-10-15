using System.Collections.Generic;

using GalaxySeeker;
using GalaxySeeker.Attack;

using UnityEngine;
[System.Serializable]
/// <summary>define how KingCannibalFlower attack target</summary>
public class KingCannibalFlowerAttack : CharacterComponent {
    //-----ref
    //store all collider of vines
    List<Collider2D> vines = new List<Collider2D> ( );
    List<Collider2D> needles = new List<Collider2D> ( );
    AttackSet vineSting; //special attack
    AttackSet vineScratch; //special attack
    AttackSet bite; //normal attack
    AttackSet vine;
    AttackSet needle;
    LayerMask targetLayer;
    float normalProbability; //probability to use normal attack
    float specialProbability; //probability to use special attack
    //------field
    //store own reference
    KingCannibalFlower kcf = null;
    Character target = null;
    Transform targetTransform = null;
    AttackSet currentAttack = null;
    List<AttackSet> specialAttacks = new List<AttackSet> ( );
    List<AttackSet> normalAttacks = new List<AttackSet> ( );
    List<AttackSet> attacks = new List<AttackSet> ( );
    bool bFacingRight;
    public KingCannibalFlowerAttack (Enemy parent, LayerMask targetLayer, float normalProbability, float specialProbability, AttackValue vineStingValue, AttackValue vineScratchValue, AttackValueRadius biteValue, AttackValueRadius vineValue, AttackValueRadius needleValue, List<Collider2D> vines, List<Collider2D> needles) : base (parent) {
        //set own to kcf
        kcf = (KingCannibalFlower) Parent;
        //set targetLayer and add vines collider and probability
        this.targetLayer = targetLayer;
        this.vines.AddRange (vines);
        this.needles.AddRange (needles);
        this.normalProbability = normalProbability;
        this.specialProbability = specialProbability;
        //instantiation all attack and add to attacks
        attacks.Add (this.vineSting = new AttackSet (vineStingValue, new BasicAttack (vineStingValue.CD, VineStingAttacking)));
        attacks.Add (this.vineScratch = new AttackSet (vineScratchValue, new BasicAttack (vineScratchValue.CD, VineScratchAttacking)));
        attacks.Add (this.bite = new AttackSet (biteValue, new CircleAreaAttack (biteValue.CD, biteValue.DetectRadius, targetLayer)));
        attacks.Add (this.vine = new AttackSet (vineValue, new CircleAreaAttack (vineValue.CD, vineValue.DetectRadius, targetLayer, VineAttacking)));
        attacks.Add (this.needle = new AttackSet (needleValue, new CircleAreaAttack (needleValue.CD, needleValue.DetectRadius, targetLayer, NeedleAttacking)));
        //add attack to one they belong to
        specialAttacks.Add (vineScratch);
        specialAttacks.Add (vineSting);
        normalAttacks.Add (bite);
        normalAttacks.Add (needle);
        normalAttacks.Add (vine);
        //register the OnAnimationFinished on Parent for AnimFinished
        kcf.OnAnimationFinished += AnimFinished;
        //try to find the player on the scene
        GalaxySeeker.Physics2D.OverlapCircle (Parent.tf.position, Mathf.Infinity, targetLayer, ref targetTransform);
        if (targetTransform)
            target = targetTransform.GetComponent<Character> ( );
    }

    protected override void FixedTick ( ) {
        if (target)
            bFacingRight = GalaxySeeker.Physics2D.IsRight (Parent.tf.position, targetTransform.position);
        //Change render direction
        if (currentAttack == null)
            Render.ChangeDirectionY (bFacingRight, Parent.tf, true);
        //Update all attacks' state
        foreach (AttackSet attack in attacks)
            attack.action.UpdateState (Parent.tf.position);
        //if not attacking try to find an attack
        if (currentAttack == null) {
            //Choose use special attack or normal attack
            bool bUseSpecial = Eccentric.Math.ChosenDueToProbability (specialProbability, normalProbability);
            //Special attack
            if (bUseSpecial) {
                //add all the attack already to list
                List<AttackSet> actionToUse = new List<AttackSet> ( );
                foreach (AttackSet attack in specialAttacks)
                    if (attack.action.IsCanAttack) actionToUse.Add (attack);
                // if there is only one set current attack to it
                // or choose it  with random with same probability
                if (actionToUse.Count == 1) currentAttack = actionToUse [0];
                else if (actionToUse.Count == 0) return;
                else currentAttack = actionToUse [Eccentric.Math.RandomNum (actionToUse.Count)];
            }
            //Normal attack
            else {
                //NOTFIN
                if (bite.action.IsCanAttack)
                    currentAttack = bite;
                else {
                    List<AttackSet> actionAbleUse = new List<AttackSet> ( );
                    if (vine.action.IsCanAttack) actionAbleUse.Add (vine);
                    if (needle.action.IsCanAttack) actionAbleUse.Add (needle);
                    if (actionAbleUse.Count == 1) currentAttack = actionAbleUse [0];
                    else if (actionAbleUse.Count == 0) return;
                    else currentAttack = actionAbleUse [Eccentric.Math.RandomNum (actionAbleUse.Count)];

                }
                if (currentAttack == null) return;
            }
            //if attack is ready attack and play the attack animation
            if (currentAttack.action.IsCanAttack) {
                currentAttack.action.Attack ( );
                kcf.Anim.Play (currentAttack.value.Clip.name);
            }
            else currentAttack = null;
        }
    }

    //define the action during vineStingAttack
    void VineStingAttacking ( ) {
        DetectTargetCollide (this.vineSting, this.targetLayer, vines);
    }

    //define the action during vineSractchAttack
    void VineScratchAttacking ( ) {
        DetectTargetCollide (this.vineScratch, this.targetLayer, vines);
    }

    //define the action during vineAttack
    void VineAttacking ( ) {
        DetectTargetCollide (this.vine, this.targetLayer, vines);
    }

    //define the action during needleAttack
    void NeedleAttacking ( ) {
        DetectTargetCollide (this.needle, this.targetLayer, needles);
    }

    //detect if targetLayer get collide with vines 
    //if true cause damage to target due to attack damage
    void DetectTargetCollide (AttackSet attack, LayerMask targetLayer, List<Collider2D> colliders) {
        //get all collider which collide with vine in all vines
        foreach (Collider2D col in colliders) {
            List<Collider2D> cols = new List<Collider2D> ( );
            ContactFilter2D filter = new ContactFilter2D ( );
            filter.SetLayerMask (targetLayer);
            col.OverlapCollider (filter, cols);
            foreach (Collider2D co in cols)
                if (target) target.TakeDamage (attack.value.Damage);
        }
    }

    //callback method for OnAnimationFinished
    //take different action due to animation end
    void AnimFinished (AnimationClip anim) {
        if (anim.name == bite.value.Clip.name)
            target.TakeDamage (this.bite.value.Damage);
        if (anim == currentAttack.value.Clip)
            currentAttack.action.AttackFinished ( );
        else return;
        kcf.Anim.Play (kcf.Anim.clip.name);
        currentAttack = null;
    }

}
