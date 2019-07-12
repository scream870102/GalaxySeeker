using UnityEngine;
namespace Eccentric.UnityUtils.Attack {
    /// <summary>This class define how an attack work with a circle detect area</summary>
    /// <remarks>Call UpdateState to update you can check if can attack now by call props IsCanAttack and will enter cooldown after you call method Attack</remarks>
    [System.Serializable]
    public class CircleAreaAttack : IAttackAction {
        //---ref
        float radius;
        float cd;
        LayerMask targetLayer;

        bool bCanAttack = false;
        /// <summary>if attack not in cooldown and target is in the range then return true otherwise return false</summary>
        public bool IsCanAttack { get { return bCanAttack; } }
        float cdRemain;
        /// <summary>return remain cooldown time 0=you can attack infinity=not find the target</summary>
        public float CDRemain { get { return cdRemain; } }
        // field to store the targetCharacter
        Character targetCharacter = null;
        // bool for find the target 
        bool bFindTarget = false;
        //field to store the target's reference 
        Transform target = null;
        bool bCDing = false;
        //to calculate cooldown
        float timer;
        Vector3 originPos;
        /// <summary>return the distance between target and owner</summary>
        public float DistanceBetweenTarget {
            get {
                if (target)
                    return Vector2.Distance (originPos, target.position);
                return Mathf.Infinity;
            }
        }

        public CircleAreaAttack (float radius, float cd, LayerMask layer) {
            this.radius = radius;
            this.cd = cd;
            this.targetLayer = layer;
            cdRemain = Mathf.Infinity;
        }

        /// <summary>Update the state of this attack action</summary>
        /// <param name="originPos">the position of owner</param>
        public void UpdateState (Vector3 originPos) {
            this.originPos = originPos;
            //Find Target
            if (!bFindTarget) {
                bFindTarget = Eccentric.UnityUtils.Physics2D.OverlapCircle (originPos, radius, targetLayer, ref target);
            }
            //Not in cd and got target then can attack
            else if (bFindTarget && !bCDing) {
                targetCharacter = target.gameObject.GetComponent<Character> ( );
                bCanAttack = true;
                cdRemain = 0f;
            }
            //Already Attack go in to cd
            else if (!bCanAttack && bCDing) {
                CoolDown ( );
            }
        }

        void CoolDown ( ) {
            if (Time.time > timer) {
                bCanAttack = true;
                bCDing = false;
            }
            cdRemain = timer - Time.time;
        }

        /// <summary>Call this method to attack the targetCharacter</summary>
        /// <param name="damage">the damage you want to add to targetCharacter</param>
        public void Attack (float damage) {
            targetCharacter.Stats.TakeDamage (damage);
            bCanAttack = false;
            bCDing = true;
            bFindTarget = false;
            timer = Time.time + cd;
        }
    }
}
