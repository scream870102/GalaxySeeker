using UnityEngine;
using Eccentric.Utils;
namespace GalaxySeeker.Attack {
    [System.Serializable]
    /// <summary>interface for attack action</summary>
    public abstract class AAttack {
        //-------------field
        bool bCanAttack = false;
        EAttackState state;
        CountdownTimer counter;
        float cd = 0f;
        event System.Action attackFinishedAction;
        //-------------property
        /// <summary>define if character can use this action to attack right now</summary>
        public bool IsCanAttack { protected set { bCanAttack = value; } get { return bCanAttack; } }
        /// <summary>define remain time of cooldown</summary>
        public float CDRemain {
            get {
                if (state == EAttackState.COOLDOWN) return counter.Remain;
                else if (state == EAttackState.WAITING) return 0f;
                else return Mathf.Infinity;
            }
        }
        //-------------method
        public AAttack (float cd, System.Action attackFinishedAction = null, bool IsCanAttack = true) {
            this.cd = cd;
            this.bCanAttack = IsCanAttack;
            this.attackFinishedAction += attackFinishedAction;
            this.state = EAttackState.WAITING;
            counter=new CountdownTimer(cd);
        }

        /// <summary>Call this method to update all the information in this attack</summary>
        public virtual void UpdateState (Vector2 originPos) {
            switch (state) {
                case EAttackState.WAITING:
                    IsPossibleAttack (originPos);
                    break;
                case EAttackState.ATTACKING:
                    Attacking ( );
                    break;
                case EAttackState.COOLDOWN:
                    CoolDown ( );
                    break;
            }
        }

        /// <summary>if possible use attack </summary>
        /// <remarks>if true modify IsCanAttack=true</remarks>
        protected abstract void IsPossibleAttack (Vector2 originPos);

        /// <summary>define the action  when outer class call attack</summary>
        /// <remarks>if attack finished call AttackFinished will enter cd can call by outer class or animator</remarks>
        protected abstract void Attacking ( );
        /// <summary>Call this method to cause Damage</summary>
        public void Attack ( ) {
            state = EAttackState.ATTACKING;
            bCanAttack = false;
        }
        /// <summary>MUST call this method to enter cooldown</summary>
        public void AttackFinished ( ) {
            if (attackFinishedAction != null)
                attackFinishedAction ( );
            state = EAttackState.COOLDOWN;
            counter.Reset();
        }

        /// <summary>call this method to deal damage to target character</summary>
        public void CauseDamage (Character target, float damage) {
            target.Stats.TakeDamage (damage);
        }

        //calculate cooldown
        void CoolDown ( ) {
            if (counter.IsFinished)
                state = EAttackState.WAITING;
        }
        protected enum EAttackState {
            WAITING,
            ATTACKING,
            COOLDOWN,

        }

    }
}
