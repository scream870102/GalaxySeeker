using UnityEngine;
namespace Eccentric.UnityUtils.Attack {
    /// <summary>the basic information for an attack movement</summary>
    /// <remarks>include damage cooldown and animation</remarks>
    [System.Serializable]
    public class AttackValue {
        public float Damage;
        public float CD;
        public AnimationClip Clip;
        public AttackValue (float Damage = 0f, float CD = 0f, AnimationClip Clip = null) {
            this.Damage = Damage;
            this.CD = CD;
            this.Clip = Clip;
        }
    }
    /// <summary>besides basic attackValue also include the radius for attackRange</summary>
    [System.Serializable]
    public class AttackValueRadius : AttackValue {
        public float DetectRadius;
        public AttackValueRadius (float Damage = 0f, float CD = 0f, AnimationClip Clip = null, float DetectRadius = 0f) : base (Damage, CD, Clip) {
            this.DetectRadius = DetectRadius;
        }
    }

    /// <summary>include action and value for an attack movement</summary>
    public class AttackSet {
        public AttackValue value;
        public AAttack action;
        public AttackSet (AttackValue value, AAttack action) {
            this.value = value;
            this.action = action;
        }
    }
}
