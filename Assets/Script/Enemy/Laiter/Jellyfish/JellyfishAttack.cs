using Eccentric.UnityUtils.Attack;

using UnityEngine;
[System.Serializable]
/// <summary>define how jellyfish attack target</summary>
public class JellyfishAttack : CharacterComponent {
    //----ref
    LayerMask targetLayer;

    //----field
    AttackSet tentacle;
    Jellyfish jf;

    public JellyfishAttack (Enemy parent, AttackValueRadius tentacle, LayerMask targetLayer) : base (parent) {
        this.tentacle = new AttackSet (tentacle, new CircleAreaAttack (tentacle.CD, tentacle.DetectRadius, targetLayer));
        this.tentacle.value = tentacle;
        this.targetLayer = targetLayer;
        jf = (Jellyfish) Parent;
        jf.OnTentacleAnimFined += TentacleAnimationFin;
    }
    protected override void FixedTick ( ) {
        tentacle.action.UpdateState (Parent.tf.position);
        if (tentacle.action.IsCanAttack) {
            tentacle.action.Attack ( );
            //播放動畫 訂閱事件
            jf.Anim.Play (tentacle.value.Clip.name);
        }
    }

    void TentacleAnimationFin ( ) {
        jf.Anim.Play (jf.Anim.clip.name);
        tentacle.action.CauseDamage ((tentacle.action as CircleAreaAttack).Target, tentacle.value.Damage);
        tentacle.action.AttackFinished ( );
    }

}
