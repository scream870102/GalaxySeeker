using UnityEngine;
/// <summary>RedAirStingray</summary>
namespace GalaxySeeker.Enemy.RedAirStingray {
    [System.Serializable]
    public class RASRandMove : ARedAirStingrayComponent {
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<RedAirStingray> ( );
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
    }
}
