using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
/// <summary>define how CannibalFlower attack target</summary>
public class CannibalFlowerAttack : CharacterComponent {
    //-----ref
    float detectRadius;
    float cooldown;
    LayerMask targetLayer;
    float timer;
    int damage;

    //------field
    //target to attack
    Transform target = null;
    //ref for target Player
    Player targetPlayer = null;
    bool bFindTarget = false;
    bool bAttack = false;

    public CannibalFlowerAttack (Enemy parent, float detectRadius, float cooldown, int damage, LayerMask targetLayer) : base (parent) {
        this.detectRadius = detectRadius;
        this.cooldown = cooldown;
        this.damage = damage;
        this.targetLayer = targetLayer;
    }

    protected override void Tick ( ) {
        //if find target and not attack try to attack it
        if (bFindTarget && !bAttack) {
            Attack ( );
            bAttack = true;
            timer = Time.time + cooldown;
        }
        //if already attack keep counting cooldown
        else if (bAttack && bFindTarget) {
            if (Time.time > timer) {
                bFindTarget = false;
                bAttack = false;
            }
        }
    }

    protected override void FixedTick ( ) {
        //Try to find target if we can attack someone
        if (!bFindTarget && !bAttack)
            target = FindTarget.CircleCast (Parent.tf.position, detectRadius, targetLayer, target);
        bFindTarget = (target? true : false);
    }

    void Attack ( ) {
        if (!targetPlayer)
            targetPlayer = target.gameObject.GetComponent<Player> ( );
        if (targetPlayer)
            targetPlayer.Stats.TakeDamage (damage);
    }
}
