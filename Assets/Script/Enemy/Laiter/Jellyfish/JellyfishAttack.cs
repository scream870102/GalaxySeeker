using Eccentric.UnityUtils.Attack;

using UnityEngine;
[System.Serializable]
/// <summary>define how jellyfish attack target</summary>
public class JellyfishAttack : CharacterComponent {
    //----ref
    AttackValueRadius tentacleValue;
    LayerMask targetLayer;
    AnimationClip attackAnim;

    //----field
    CircleAreaAttack tentacleAction = null;
    Jellyfish jf;

    public JellyfishAttack (Enemy parent, AttackValueRadius tentacle, LayerMask targetLayer, AnimationClip attackAnimClip) : base (parent) {
        this.tentacleValue = tentacle;
        this.targetLayer = targetLayer;
        this.attackAnim = attackAnimClip;
        tentacleAction = new CircleAreaAttack (tentacle.CD, tentacleValue.DetectRadius, targetLayer);
        jf = (Jellyfish) Parent;
        jf.OnTentacleAnimFined += TentacleAnimationFin;
    }
    protected override void FixedTick ( ) {
        tentacleAction.UpdateState (Parent.tf.position);
        if (tentacleAction.IsCanAttack) {
            tentacleAction.Attack ( );
            //播放動畫 訂閱事件
            jf.Anim.Play (attackAnim.name);
        }
    }

    void TentacleAnimationFin ( ) {
        jf.Anim.Play (jf.Anim.clip.name);
        tentacleAction.CauseDamage (tentacleValue.Damage);
        tentacleAction.AttackFinished ( );
    }

}
