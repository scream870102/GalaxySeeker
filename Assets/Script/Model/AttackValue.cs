using UnityEngine;
namespace Eccentric.UnityUtils.Attack {
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
    /// <summary>struct define the basic information for an attack action</summary>
    /// <remarks>include damage,cd,and detectRadius for circleCast</remarks>
    [System.Serializable]
    public class AttackValueRadius : AttackValue {
        public float DetectRadius;
        public AttackValueRadius (float Damage = 0f, float CD = 0f, AnimationClip Clip = null, float DetectRadius = 0f) : base (Damage, CD, Clip) {
            this.DetectRadius = DetectRadius;
        }
    }

    public class AttackPair {
        public AttackValue value;
        public AAttack action;
        public AttackPair (AttackValue value, AAttack action) {
            this.value = value;
            this.action = action;
        }
    }
}
