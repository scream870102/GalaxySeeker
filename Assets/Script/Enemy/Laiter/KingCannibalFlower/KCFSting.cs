using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFSting : AKingCannibalFlowerComponent {
        bool bTouched = false;
        [SerializeField] Vector2 force = Vector2.zero;
        [SerializeField] float damage = 0f;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<KingCannibalFlower> ( );
            bTouched = false;
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!bTouched) {
                int hits = Parent.VineColTrigger ( );
                if (hits > 0) {
                    bTouched = true;
                    Parent.Player.AddRelativeForce (force);
                    Parent.Player.TakeDamage (damage);
                }
            }
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
    }
}
