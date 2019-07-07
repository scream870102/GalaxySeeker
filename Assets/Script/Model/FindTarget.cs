using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Eccentric.UnityModel {
    [System.Serializable]
    /// <summary>There are some static methods to check if target in area</summary>
    public static class FindTarget {

        /// <summary>Use circleCast to find target return null</summary>
        /// <remarks>if there is no target found will return null</remarks>
        /// <param name="originPos">the center of circle</param>
        /// <param name="radius">the radius of detect circle</param>
        /// <param name="targetLayer">what layerMask should react</param>
        /// <param name="target">if target already exist will check distance between center poiint and target is less than or equal to radius or not</param>
        public static Transform CircleCast (Vector2 originPos, float radius, LayerMask targetLayer, Transform target = null) {
            if (!target) {
                RaycastHit2D hit = Physics2D.CircleCast (originPos, radius, Vector2.zero, 0f, targetLayer.value);
                if (hit) {
                    return hit.transform;
                }
            }
            else {
                if (Vector2.Distance (target.position, originPos) <= radius) {
                    return target;
                }
            }
            return null;
        }
    }
}
