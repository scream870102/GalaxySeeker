using System.Collections;
using System.Collections.Generic;

using Eccentric;

using UnityEngine;
namespace Eccentric.Attack {
    [System.Serializable]
    public class CircleAreaAttack : IAttack {
        //---ref
        float radius;
        float cd;
        LayerMask targetLayer;

        bool bCanAttack = false;
        public bool IsCanAttack { get { return bCanAttack; } }
        float cdRemain;
        public float CDRemain { get { return cdRemain; } }
        public Character TargetCharacter { get { return targetCharacter; } }
        bool bFindTarget = false;
        Transform target = null;
        [SerializeField]
        Character targetCharacter;
        bool bCDing = false;
        float timer;
        public CircleAreaAttack (float radius, float cd, LayerMask layer) {
            this.radius = radius;
            this.cd = cd;
            this.targetLayer = layer;
            cdRemain = Mathf.Infinity;
        }

        public void UpdateState (Vector3 originPos) {
            //Find Target
            if (!bFindTarget) {
                target = Eccentric.UnityModel.FindTarget.CircleCast (originPos, radius, targetLayer, target);
                bFindTarget = target?true : false;
                cdRemain = 0f;
            }
            //Not in cd and got target then can attack
            else if (bFindTarget && !bCDing) {
                targetCharacter = target.gameObject.GetComponent<Character> ( );
                bCanAttack = true;
                Debug.Log ("can attack");
            }

            //Already Attack go in to cd
            else if (!bCanAttack && bCDing) {
                CoolDown ( );
                Debug.Log ("In cd");
            }

        }

        void CoolDown ( ) {
            if (Time.time > timer) {
                bCanAttack = true;
                bCDing = false;
            }
            cdRemain = timer - Time.time;
        }

        public void Attack<T>(float damage) where T:Character{
            T t=(T)targetCharacter;
            Debug.Log(typeof(T));
            t.Stats.TakeDamage(damage);
        }
    }
}
