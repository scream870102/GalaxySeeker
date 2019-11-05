using UnityEngine;
/// <summary>define how jellyfish attack target</summary>
namespace GalaxySeeker.Enemy.Jellyfish {
    [System.Serializable]
    public class JFAttack : AJellyFishComponent {
        [SerializeField] float attackPoint=0f;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<Jellyfish> ( );
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.Player.TakeDamage (this.attackPoint);
            this.Parent.ChooseNextAction ( );
        }
    }

}
