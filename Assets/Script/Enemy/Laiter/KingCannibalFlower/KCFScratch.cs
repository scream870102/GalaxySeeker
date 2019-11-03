using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFScratch : AKingCannibalFlowerComponent {
        [SerializeField] float damage = 0f;
        bool bScratching = false;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<KingCannibalFlower> ( );
            bScratching = false;
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!bScratching) {
                int hit = Parent.VineColTrigger ( );
                if (hit > 0) {
                    Parent.Player.BeScratched (stateInfo.length - stateInfo.normalizedTime);
                    bScratching = true;
                }
            }
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
    }
}
