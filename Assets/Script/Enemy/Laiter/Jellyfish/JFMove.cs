using UnityEngine;
namespace GalaxySeeker.Enemy.Jellyfish {
    public class JFMove : AJellyFishComponent {
        float minDistanceBetweenPlayer = .1f;
        Vector2 initPos = Vector2.zero;
        Vector2 targetPos = Vector2.zero;
        Vector2 newPos = Vector2.zero;
        bool bFindNewPos = false;

        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<Jellyfish> ( );
                initPos = Parent.tf.position;
            }
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (Parent) {
                Vector2 parentPos = Parent.tf.position;
                //if player not in the trace range do random move
                if (Parent.DistanceBetweenPlayer > Parent.Props.TraceDist) {
                    if (!bFindNewPos) {
                        FindNewTargetPos ( );
                        newPos = parentPos;
                    }
                    else {
                        if (Vector2.Distance (parentPos, targetPos) < 0.1f)
                            bFindNewPos = false;
                        newPos = Vector2.MoveTowards (parentPos, targetPos, Parent.Props.NormalVel * Time.deltaTime);
                    }
                    Parent.UpdateRenderDirection ((newPos - (Vector2) Parent.tf.position).x > 0 ? true : false, true);
                }
                //if player in the trace range move toward to player
                else {
                    if (Parent.DistanceBetweenPlayer <= minDistanceBetweenPlayer)
                        newPos = parentPos;
                    else {
                        Vector2 tmp = ((Vector2) Parent.Player.tf.position - parentPos).normalized;
                        newPos = parentPos + tmp * Parent.Props.TraceVel * Time.deltaTime;
                    }
                    Parent.UpdateRenderDirectionWithPlayerPos (true);
                }
                Parent.tf.position = newPos;
            }
        }

        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Parent.ChooseNextAction ( );
        }
        void FindNewTargetPos ( ) {
            Vector2 dir = Random.insideUnitCircle;
            dir.Normalize ( );
            targetPos = initPos + dir * new Vector2 (Random.Range (0f, Parent.Props.Range.x), Random.Range (0f, Parent.Props.Range.y));
            bFindNewPos = true;
        }
    }
}
