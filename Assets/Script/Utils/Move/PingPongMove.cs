using UnityEngine;
namespace GalaxySeeker.Move {
    /// <summary>Do a pingpong movement in horizontal</summary>
    public class PingPongMove : AMove {
        //---------reference
        float velocity;
        //---------field
        // max x point will this movement reached
        float horiMinPos;
        // min x point will this movement reached
        float horiMaxPos;
        // current vertical position should this move at
        float vertPos;
        //-----------------property
        /// <summary>the vertical point of this movement</summary>
        public float VerticalPos { get { return vertPos; } }
        public PingPongMove (float Velocity, float HorizontalRange, Vector2 InitPos, bool IsFacingRight = true, bool IsUseDeltaTime = true) :base(InitPos,IsUseDeltaTime){
            this.velocity = Velocity;
            this.horiMinPos = InitPos.x - HorizontalRange;
            this.horiMaxPos = InitPos.x + HorizontalRange;
            this.bFacingRight = IsFacingRight;
        }

        /// <summary>return the next position of movement</summary>
        /// <remarks>will change direction automatically if it reached the edge</remarks>
        public override Vector2 GetNextPos (in Vector2 refPos) {
            Vector2 pos = refPos;
            pos.y = vertPos;
            if (bFacingRight) {
                pos += new Vector2 (velocity * (bUseDeltaTime?Time.deltaTime : Time.fixedDeltaTime), 0f);
                if (pos.x >= horiMaxPos)
                    bFacingRight = false;

            }
            else {
                pos -= new Vector2 (velocity * (bUseDeltaTime?Time.deltaTime : Time.fixedDeltaTime), 0f);
                if (pos.x <= horiMinPos)
                    bFacingRight = true;
            }
            vertPos = pos.y;
            return pos;
        }

        /// <summary>change the vertical point of this movement</summary>
        /// <param name="newVerticalPos">the new point of vertical point</param>
        public void SetVerticalPos (float newVerticalPos) {
            vertPos = newVerticalPos;
        }
    }
}
