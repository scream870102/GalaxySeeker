using UnityEngine;
/// <summary>CannibalFlower</summary>
namespace GalaxySeeker.Enemy.CannibalFlower {
    [System.Serializable]
    public class CFIdle : ACannibalFlowerComponent {
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<CannibalFlower> ( );
            Parent.UpdateRenderDirectionWithPlayerPos (true);
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
    }
}
