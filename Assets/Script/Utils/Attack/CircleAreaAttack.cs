using UnityEngine;
namespace GalaxySeeker.Attack {
    /// <summary>This class define how an attack work with a circle detect area</summary>
    /// <remarks>Call UpdateState to update you can check if can attack now by call props IsCanAttack and will enter cooldown after you call method Attack</remarks>
    [System.Serializable]
    public class CircleAreaAttack : BasicAttack {
        //------ref
        float radius;
        LayerMask targetLayer;
        //------field
        bool bFindTarget = false;
        Transform targetTransform = null;
        Vector2 originPos;
        Character target = null;
        public Character Target { get { return target; } }
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
        public CircleAreaAttack (float cd, float radius, LayerMask targetLayer, System.Action attackingAction = null, System.Action attackFinishedAction = null, bool IsCanAttack = false) : base (cd, attackingAction, attackFinishedAction, IsCanAttack) {
            this.radius = radius;
            this.targetLayer = targetLayer;
        }
        protected override void IsPossibleAttack (Vector2 originPos) {
            //try to find the target 
            //if target in the range will set IsCanAttack to true else to false
            this.originPos = originPos;
            bFindTarget = GalaxySeeker.Physics2D.OverlapCircle (originPos, radius, targetLayer, ref targetTransform);
            //if got the  target
            if (bFindTarget) {
                target = targetTransform.GetComponent<Character> ( );
                IsCanAttack = target?true : false;
            }
            else {
                IsCanAttack = false;
            }
        }
        
        public void CauseDamage (float damage) {
            if (target)
                CauseDamage (target, damage);
        }
    }
}
