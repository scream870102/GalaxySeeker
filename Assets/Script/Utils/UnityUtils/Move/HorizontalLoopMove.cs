using UnityEngine;
namespace Eccentric.UnityUtils.Move {
    /// <summary>Do a pingpong movement in horizontal</summary>
    public class HorizontalLoopMove {
        //---------reference
        float velocity;
        //true calculate with deltaTime false calculate with fixedDeltaTime
        bool bUsingDeltaTime;
        //---------field
        bool bEnable = true;
        /// <summary>Is this movement enable or disable</summary>
        public bool IsEnable { get { return bEnable; } set { bEnable = value; } }
        // max x point will this movement reached
        float horiMinPos;
        // min x point will this movement reached
        float horiMaxPos;
        // field to store currentPosition
        Vector2 pos;
        bool bFacingRight;
        /// <summary>if this movement facing at right or not</summary>
        public bool IsFacingRight { get { return bFacingRight; } set { bFacingRight = value; } }
        /// <summary>the vertical point of this movement</summary>
        public float VerticalPos { get { return pos.y; } }
        public HorizontalLoopMove (float velocity, float horizontalRange, Vector2 initPos, bool IsFacingRight = true, bool IsUsingDeltaTime = true, bool IsEnable = true) {
            this.velocity = velocity;
            this.horiMinPos = initPos.x - horizontalRange;
            this.horiMaxPos = initPos.x + horizontalRange;
            this.bEnable = IsEnable;
            this.pos = initPos;
            this.bFacingRight = IsFacingRight;
            this.bUsingDeltaTime = IsUsingDeltaTime;
        }

        /// <summary>return the next position of movement</summary>
        /// <remarks>will change direction automatically if it reached the edge</remarks>
        public Vector2 GetNextPos ( ) {
            if (bEnable) {
                if (bFacingRight) {
                    pos += new Vector2 (velocity * (bUsingDeltaTime?Time.deltaTime : Time.fixedDeltaTime), 0f);
                    if (pos.x >= horiMaxPos)
                        bFacingRight = false;

                }
                else {
                    pos -= new Vector2 (velocity * (bUsingDeltaTime?Time.deltaTime : Time.fixedDeltaTime), 0f);
                    if (pos.x <= horiMinPos)
                        bFacingRight = true;
                }
            }
            return pos;
        }

        /// <summary>change the vertical point of this movement</summary>
        /// <param name="newVerticalPos">the new point of vertical point</param>
        public void SetVerticalPos (float newVerticalPos) {
            pos.y = newVerticalPos;
        }
    }
}
