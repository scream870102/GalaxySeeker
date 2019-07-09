using Eccentric.UnityUtils.Attack;
using UnityEngine;
[System.Serializable]
/// <summary>define how jellyfish attack target</summary>
public class JellyfishAttack : CharacterComponent {
    //----ref
    AttackValue tentacleValue;
    LayerMask targetLayer;

    //----field
    [SerializeField]
    CircleAreaAttack tentacleAction = null;

    public JellyfishAttack (Enemy parent, AttackValue tentacle, LayerMask targetLayer) : base (parent) {
        this.tentacleValue = tentacle;
        this.targetLayer = targetLayer;
        tentacleAction = new CircleAreaAttack (tentacleValue.DetectRadius, tentacle.CD, targetLayer);
    }
    protected override void FixedTick ( ) {
        tentacleAction.UpdateState (Parent.tf.position);
        if (tentacleAction.IsCanAttack) {
            tentacleAction.Attack (tentacleValue.Damage);
        }
    }

}
