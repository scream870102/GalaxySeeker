using UnityEngine;

namespace GalaxySeeker {
    [System.Serializable]
    /// <summary>Static method about some calculate for 2d coordinate</summary>
    public static class Physics2D {
        /// <summary>Use OverlapCircle to find target return null</summary>
        /// <remarks>if there is no target found will return null</remarks>
        /// <param name="originPos">the center of circle</param>
        /// <param name="radius">the radius of detect circle</param>
        /// <param name="targetLayer">what layerMask should react</param>
        /// <param name="target">if target already exist will check distance between center poiint and target is less than or equal to radius or not</param>
        public static bool OverlapCircle (Vector2 originPos, float radius, LayerMask targetLayer, ref Transform target) {
            if (!target) {
                //RaycastHit2D hit = UnityEngine.Physics2D.CircleCast (originPos, radius, Vector2.zero, 0f, targetLayer.value);
                Collider2D col = UnityEngine.Physics2D.OverlapCircle (originPos, radius, targetLayer.value);
                if (col) {
                    target = col.transform;
                    return true;
                }
            }
            else {
                if (Vector2.Distance (target.position, originPos) <= radius) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Use circleCast to find target return null</summary>
        /// <remarks>if there is no target found will return null</remarks>
        /// <param name="originPos">the center of circle</param>
        /// <param name="radius">the radius of detect circle</param>
        /// <param name="direction">the direction of detect circle spawn</param>
        /// <param name="distance">how far from the originPos to the last circle on the direction</param>
        /// <param name="targetLayer">what layerMask should react</param>
        public static bool CircleCast (Vector2 originPos, float radius, Vector2 direction, float distance, LayerMask targetLayer, ref Transform target) {
            RaycastHit2D hit = UnityEngine.Physics2D.CircleCast (originPos, radius, direction, distance, targetLayer.value);
            if (hit) {
                target = hit.transform;
                return true;
            }
            return false;
        }

        /// <summary>Check if other point above original point</summary>
        /// <param name="original">the center point you want to check</param>
        /// <param name="target">the target point you want to check</param>
        /// <param name="range">the range of original object DEFAULT=INFINITY</param>
        public static bool IsAbove (Vector2 original, Vector2 target, float range = Mathf.Infinity) {
            Vector2 offset = target - original;
            if (offset.y > 0f) return Mathf.Abs (offset.x) < range / 2f?true : false;
            else return false;
        }

        /// <summary>Check if other point under original point</summary>
        /// <param name="original">the center point you want to check</param>
        /// <param name="target">the target point you want to check</param>
        /// <param name="range">the range of original object DEFAULT=INFINITY</param>
        public static bool IsUnder (Vector2 original, Vector2 target, float range = Mathf.Infinity) {
            Vector2 offset = target - original;
            if (offset.y < 0f) return Mathf.Abs (offset.x) < range / 2f?true : false;
            else return false;
        }

        /// <summary>Check if other point at right of original point</summary>
        /// <param name="original">the center point you want to check</param>
        /// <param name="target">the target point you want to check</param>
        /// <param name="range">the range of original object DEFAULT=INFINITY</param>
        public static bool IsRight (Vector2 original, Vector2 target, float range = Mathf.Infinity) {
            Vector2 offset = target - original;
            if (offset.x > 0f) return Mathf.Abs (offset.y) < range / 2f?true : false;
            else return false;
        }

        /// <summary>Check if other point at left original point</summary>
        /// <param name="original">the center point you want to check</param>
        /// <param name="target">the target point you want to check</param>
        /// <param name="range">the range of original object DEFAULT=INFINITY</param>
        public static bool IsLeft (Vector2 original, Vector2 target, float range = Mathf.Infinity) {
            Vector2 offset = target - original;
            if (offset.x < 0f) return Mathf.Abs (offset.y) < range / 2f?true : false;
            else return false;
        }

    }
}
