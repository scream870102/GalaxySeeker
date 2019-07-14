using UnityEngine;
namespace Eccentric.UnityUtils.Move {
    /// <summary>Do a trace movement without any collision</summary>
    public class FreeTraceMove : IMove {
        //---------reference
        float velocity;
        Transform target = null;
        //-----------------property
        public Transform Target { get { return target; } }
        public FreeTraceMove (float Velocity, Transform Target, Vector2 InitPos, bool IsUseDeltaTime = true) : base (InitPos, IsUseDeltaTime) {
            this.velocity = Velocity;
            this.target = Target;
        }

        /// <summary>return the next position of movement</summary>
        public override Vector2 GetNextPos (in Vector2 refPos) {
            Vector2 pos = refPos;
            if (target) {
                pos = (Vector2) target.position - refPos;
                float speed = bUseDeltaTime?Time.deltaTime : Time.fixedDeltaTime;
                pos = refPos + pos.normalized * speed;
            }
            bFacingRight = (pos - refPos).x > 0f;
            return pos;
        }

        /// <summary>change the trace target</summary>
        /// <param name="target">the one you want to trace</param>
        public void SetTarget (Transform target) {
            this.target = target;
        }

    }
}
