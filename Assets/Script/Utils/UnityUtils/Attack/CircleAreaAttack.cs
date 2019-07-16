using UnityEngine;
namespace Eccentric.UnityUtils.Attack {
    /// <summary>This class define how an attack work with a circle detect area</summary>
    /// <remarks>Call UpdateState to update you can check if can attack now by call props IsCanAttack and will enter cooldown after you call method Attack</remarks>
    [System.Serializable]
    public class CircleAreaAttack : AAttack {
        //------ref
        float radius;
        LayerMask targetLayer;
        //------field
        bool bFindTarget = false;
        Transform targetTransform = null;
        Vector2 originPos;
        Character target = null;
        //------property
        /// <summary>return the distance between target and owner</summary>
        public float DistanceBetweenTarget {
            get {
                if (targetTransform)
                    return Vector2.Distance (originPos, targetTransform.position);
                return Mathf.Infinity;
            }
        }
        //-----method
        public CircleAreaAttack (float cd, float radius, LayerMask targetLayer, System.Action attackFinishedAction = null, bool IsCanAttack = false) : base (cd, attackFinishedAction, IsCanAttack) {
            this.radius = radius;
            this.targetLayer = targetLayer;
        }
        protected override void IsPossibleAttack (Vector2 originPos) {
            this.originPos = originPos;
            bFindTarget = Eccentric.UnityUtils.Physics2D.OverlapCircle (originPos, radius, targetLayer, ref targetTransform);
            if (bFindTarget) {
                target = targetTransform.GetComponent<Character> ( );
                IsCanAttack = target?true : false;
            }
            else {
                IsCanAttack = false;
            }
        }
        protected override void Attacking ( ) { }
        public void CauseDamage (float damage) {
            if (target)
                CauseDamage (target, damage);
        }
    }
}
