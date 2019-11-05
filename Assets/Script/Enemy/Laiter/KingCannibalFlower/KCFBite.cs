using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFBite : AKingCannibalFlowerComponent {
        [SerializeField] float damage = 0f;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<KingCannibalFlower> ( );
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Parent.Player.TakeDamage (damage);
            this.Parent.ChooseNextAction ( );
        }
    }
}
