using UnityEngine;
namespace GalaxySeeker.Attack {
    [System.Serializable]
    public class BasicAttack : AAttack {
        System.Action attackingAction = null;

        /// <summary>the constructor of basic attack it strong connect with animation</summary>
        /// <param name="attackingAction">the action should do</param>
        public BasicAttack (float cd, System.Action attackingAction, System.Action attackFinishedAction = null, bool IsCanAttack = true) : base (cd, attackFinishedAction, IsCanAttack) {
            this.attackingAction += attackingAction;
        }

        protected override void IsPossibleAttack (Vector2 originPos) {
            IsCanAttack = true;
        }
        protected override void Attacking ( ) {
            if (attackingAction != null)
                attackingAction ( );
        }

    }
}
