using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
/// <summary>define how jellyfish attack target</summary>
public class JellyfishAttack : CharacterComponent {
    // if jellyfish find attack target
    bool bFindTarget = false;
    // ref for target transform
    Transform target = null;
    // ref for target Player
    Player targetPlayer = null;
    // radius of jellyfish attack circle
    float detectAreaRadius;
    // how many damage will cause in one hit
    int damage;
    // if jellyfish already attack target
    bool bAttack = false;
    // timer to calculate skill cooldown
    float timer = 0f;
    // interval time between two attack 
    float coolDown;
    LayerMask targetLayer;

    public JellyfishAttack (Enemy parent, float detectArea, int damage, float coolDown, LayerMask targetLayer) : base (parent) {
        this.detectAreaRadius = detectArea;
        this.damage = damage;
        this.coolDown = coolDown;
        this.targetLayer = targetLayer;
    }

    protected override void Tick ( ) {
        // if already find target then attack it
        if (bFindTarget && !bAttack) {
            Attack ( );
            bAttack = true;
            timer = Time.time + coolDown;
        }
        // find target and already attack then become skill cd
        else if (bAttack && bFindTarget) {
            if (Time.time > timer) {
                bFindTarget = false;
                bAttack = false;
            }
        }
    }

    protected override void FixedTick ( ) {
        if (!bFindTarget && !bAttack)
            target = FindTarget.CircleCast (Parent.tf.position, detectAreaRadius, targetLayer, target);
        bFindTarget = (target? true : false);

    }

    // if Find Target then attack it
    void Attack ( ) {
        if (!targetPlayer)
            targetPlayer = target.gameObject.GetComponent<Player> ( );
        if (targetPlayer)
            targetPlayer.Stats.TakeDamage (damage);
    }
}
