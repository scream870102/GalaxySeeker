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
    // if jellyfish alreay attack target
    bool bAttack = false;
    // timer to calculate skill cooldown
    float timer = 0f;
    // interval time between two attack 
    float coolDown;

    public JellyfishAttack (Enemy parent, float detectArea, int damage, float coolDown) : base (parent) {
        this.detectAreaRadius = detectArea;
        this.damage = damage;
        this.coolDown = coolDown;
    }

    protected override void Tick ( ) {
        // if alreay find target then attack it
        if (bFindTarget && !bAttack) {
            Attack ( );
            bAttack = true;
            timer = Time.time + coolDown;
        }
        // find target and alreay attack then become skill cd
        else if (bAttack && bFindTarget) {
            if (Time.time > timer) {
                bFindTarget = false;
                bAttack = false;
            }
        }
    }

    protected override void FixedTick ( ) {
        if (!bFindTarget && !bAttack)
            bFindTarget = FindTartget ( );
    }

    // Use circleCast to find target 
    // if alreay find target use distance to check if player in attack area
    bool FindTartget ( ) {
        if (!target) {
            RaycastHit2D hit = Physics2D.CircleCast (Parent.tf.position, detectAreaRadius, Vector2.zero, 0f, 1 << 11);
            if (hit) {
                bFindTarget = true;
                target = hit.transform;
                return true;
            }
        }
        else {
            return (Vector2.Distance (target.position, Parent.tf.position) <= detectAreaRadius);
        }
        return false;
    }

    // if Find Target then attack it
    void Attack ( ) {
        if (!targetPlayer)
            targetPlayer = target.gameObject.GetComponent<Player> ( );
        if (targetPlayer)
            targetPlayer.Stats.TakeDamage (damage);
    }
}
