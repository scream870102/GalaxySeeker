using System.Collections.Generic;

using UnityEngine;
/// <summary>define how jellyfish use its need to attack player</summary>
namespace GalaxySeeker.Enemy.CannibalFlower {
    [System.Serializable]
    public class CFNeedle : ACannibalFlowerComponent {
        [SerializeField] float attackPoint = 0f;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<CannibalFlower> ( );
            Parent.UpdateRenderDirectionWithPlayerPos ( );
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            NeedleAttacking ( );
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }

        void NeedleAttacking ( ) {
            //get all collider which collide with vine in all vines
            foreach (Collider2D needle in Parent.Needles) {
                List<Collider2D> cols = new List<Collider2D> ( );
                ContactFilter2D filter = new ContactFilter2D ( );
                filter.SetLayerMask (Parent.PlayerLayer);
                needle.OverlapCollider (filter, cols);
                foreach (Collider2D col in cols)
                    this.Parent.Player.TakeDamage (this.attackPoint);
            }
        }
    }

}
