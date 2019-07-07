using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
/// <summary>define how jellyfish attack target</summary>
public class JellyfishAttack : CharacterComponent {
    [SerializeField]
    Eccentric.Attack.CircleAreaAttack tentacleAction = null;
    AttackValue tentacleValue;
    LayerMask targetLayer;

    public JellyfishAttack (Enemy parent, AttackValue tentacle, LayerMask targetLayer) : base (parent) {
        this.tentacleValue = tentacle;
        this.targetLayer = targetLayer;
        tentacleAction = new Eccentric.Attack.CircleAreaAttack (tentacleValue.DetectRadius, tentacle.CD, targetLayer);
    }
    protected override void FixedTick ( ) {
        tentacleAction.UpdateState (Parent.tf.position);
        if (tentacleAction.IsCanAttack) {
            tentacleAction.Attack<Player>(tentacleValue.Damage);
            Player player = (Player)tentacleAction.TargetCharacter;
            player.Stats.TakeDamage (tentacleValue.Damage);
        }

    }

}
