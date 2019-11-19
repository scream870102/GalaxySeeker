using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFScratch : AKingCannibalFlowerComponent {
        [SerializeField] float damage = 0f;
        bool bScratching = false;
        Transform scratchObjectTf = null;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<KingCannibalFlower> ( );
                scratchObjectTf = Parent.ScratchASting.transform;
            }
            bScratching = false;
            scratchObjectTf.position = Parent.ScratchLaunchPoint.position;
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!bScratching) {
                int hit = Parent.ScratchAStingColTrigger ( );
                if (hit > 0) {
                    Parent.Player.BeScratched (stateInfo.length - stateInfo.normalizedTime);
                    bScratching = true;
                    Parent.Player.TakeDamage(damage);
                }
            }
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
    }
}
