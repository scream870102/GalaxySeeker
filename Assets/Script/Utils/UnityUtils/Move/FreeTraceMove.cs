using UnityEngine;
namespace Eccentric.UnityUtils.Move {
    /// <summary>Do a trace movement without any collision</summary>
    public class FreeTraceMove : AMove {
        //---------reference
        float velocity;
        Transform target = null;
        float minDistance;
        //-----------------property
        public Transform Target { get { return target; } }
        public FreeTraceMove (float Velocity, Transform Target, Vector2 InitPos, float minDistance, bool IsUseDeltaTime = true) : base (InitPos, IsUseDeltaTime) {
            this.velocity = Velocity;
            this.target = Target;
            this.minDistance = minDistance;
        }

        /// <summary>return the next position of movement</summary>
        public override Vector2 GetNextPos (in Vector2 refPos) {
            Vector2 pos = refPos;
            if (target) {
                if (Vector2.Distance (refPos, (Vector2) target.position) < minDistance) {
                    return pos;
                }
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
