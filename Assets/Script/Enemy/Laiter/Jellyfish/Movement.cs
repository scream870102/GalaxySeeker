using UnityEngine;
namespace GalaxySeeker.Enemy.Jellyfish {
    public class Movement : AEnemyComponent {
        [SerializeField] float traceDistance;
        [SerializeField] float traceSpeed;
        [SerializeField] float normalSpeed;
        [SerializeField] Vector2 range;
        public Jellyfish Parent { get { return this.parent as Jellyfish; } protected set { this.parent = value; } }
        float minDistanceBetweenPlayer = .1f;
        Vector2 initPos;
        Vector2 targetPos;
        Vector2 newPos;
        bool bFindNewPos;

        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Debug.Log ("Find Parent");
                Parent = animator.GetComponent<Jellyfish> ( );
                initPos = Parent.tf.position;
                targetPos = new Vector2 ( );
                newPos = new Vector2 ( );
                bFindNewPos = false;
            }
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Vector2 parentPos = Parent.tf.position;
                //如果玩家不在可追蹤的距離內 就隨機移動
                if (Parent.DistanceBetweenPlayer > traceDistance) {
                    if (!bFindNewPos)
                        FindNewTargetPos ( );
                    else {
                        if (Vector2.Distance (parentPos, targetPos) < 0.1f)
                            bFindNewPos = false;
                        newPos = Vector2.MoveTowards (parentPos, targetPos, normalSpeed * Time.deltaTime);
                    }
                }
                //如果玩家在可以追蹤的距離內 就往玩家移動
                else {
                    if (Parent.DistanceBetweenPlayer <= minDistanceBetweenPlayer)
                        newPos = Parent.tf.position;
                    else {
                        Vector2 tmp = ((Vector2) Parent.Player.tf.position - parentPos).normalized;
                        newPos = parentPos + tmp * Time.deltaTime;
                    }
                }
            }
        }

        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        void FindNewTargetPos ( ) {
            Vector2 dir = Random.insideUnitCircle;
            
            dir.Normalize ( );
            targetPos = initPos + dir * new Vector2 (Random.Range (0f, range.x), Random.Range (0f, range.y));
            bFindNewPos = true;
        }
        // //-----------ref
        // // move speed default value =  1.0f
        // float speed = 1f;
        // // how big of ellipse from jellyfish original pos
        // Vector2 range;
        // // how fast when jellyfish find target and try to trace it
        // float traceSpeed;
        // // Which layer to detect
        // LayerMask targetLayer;
        // // how big the circle which is to detect player
        // float detectAreaRadius;
        // float minDistance;

        // //---------field
        // // original pos of jellyfish
        // Vector2 initPos = new Vector2 ( );
        // /// <summary>target position</summary>
        // Vector2 targetPos;
        // // save ref for tracing target
        // Transform target = null;
        // // is player find target
        // bool bFindTarget = false;
        // RangeRandomMove rangeMove = null;
        // // if got the target will use traceMode
        // FreeTraceMove traceMove = null;
        // AMove currentMove = null;
        // protected override void Tick ( ) {
        //     //if find the target try to approach the target 
        //     if (bFindTarget) {
        //         currentMove = traceMove;
        //         if (traceMove.Target != target)
        //             traceMove.SetTarget (target);
        //         Parent.tf.position = traceMove.GetNextPos (Parent.tf.position);
        //     }
        //     //if not find the target just do RandomRangeMove
        //     else {
        //         currentMove = rangeMove;
        //         targetPos = rangeMove.GetNextPos (Parent.tf.position);
        //         Parent.tf.position = targetPos;
        //     }
        //     //Change jellyfish render direction
        //     Render.ChangeDirection (currentMove.IsFacingRight, Parent.tf);

        // }

    }
}
