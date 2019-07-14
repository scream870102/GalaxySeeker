using UnityEngine;
namespace Eccentric.UnityUtils.Move {
    public class RangeRandomMove : IMove {
        //--------------ref
        float speed;
        Vector2 range;
        //--------------field
        //if the direction already changed
        bool bDirChanged;
        //the target position should go
        Vector2 targetPos;
        public RangeRandomMove (Vector2 Range, float Speed, Vector2 InitPos, bool IsUseDeltaTime = true) : base (InitPos, IsUseDeltaTime) {
            this.range = Range;
            this.speed = Speed;
        }

        public override Vector2 GetNextPos (in Vector2 refPos) {
            //try to approach the target position
            //if reached the target position bDirChanged will equal to false
            if (bDirChanged) {
                if (refPos == targetPos)
                    bDirChanged = false;
                bFacingRight = (targetPos - refPos).x > 0f?true : false;
                return Vector2.MoveTowards (refPos, targetPos, speed * (bUseDeltaTime?Time.deltaTime : Time.fixedDeltaTime));
            }
            //Find a new target position
            else {
                FindNewTargetPos ( );
                bDirChanged = true;
                return refPos;
            }

        }

        //try to find a new target position
        public void FindNewTargetPos ( ) {
            Vector2 dir = Random.insideUnitCircle;
            dir.Normalize ( );
            targetPos = initPos + dir * new Vector2 (Random.Range (0f, range.x), Random.Range (0f, range.y));
        }

    }
}
