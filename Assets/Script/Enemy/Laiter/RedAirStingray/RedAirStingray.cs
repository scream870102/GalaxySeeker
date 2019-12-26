using System.Collections.Generic;

using UnityEngine;
using UE = UnityEngine;
using UP2 = UnityEngine.Physics2D;
namespace GalaxySeeker.Enemy.RedAirStingray {
    public class RedAirStingray : AEnemy {
        [SerializeField] RedAirStingrayProps props = null;
        [SerializeField] int randMoveIndex = 0;
        bool bTouchedPlayer = false;
        Vector2 initPos = Vector2.zero;
        float yUnit = 0f;
        new Collider2D collider = null;
        public Vector2 InitPos => initPos;
        public Collider2D Collider => collider;
        public RedAirStingrayProps Props => props;
        public bool IsTouchedByPlayer { set { if (value != bTouchedPlayer) { bTouchedPlayer = value; } } get { return bTouchedPlayer; } }
        void Awake ( ) {
            Init ( );
            collider = GetComponent<Collider2D> ( );
            initPos = tf.position;
            yUnit = (collider as BoxCollider2D).size.y * tf.localScale.y * 0.5f;
        }
        override protected void Dead ( ) {
            Debug.Log ("I am RedAirStingrayI come from hell");
            this.gameObject.SetActive(false);
        }
        override public void ChooseNextAction ( ) {
            //if touched by player use skinAction
            if (IsTouchedByPlayer)
                this.Animator.SetTrigger (this.Actions [randMoveIndex].Trigger);
            else
                this.Animator.ResetTrigger (this.Actions [randMoveIndex].Trigger);
        }

        public bool GetTouchPlayer ( ) {
            List<ContactPoint2D> point2Ds = new List<ContactPoint2D> ( );
            Collider.GetContacts (point2Ds);
            foreach (ContactPoint2D item in point2Ds) {
                if (item.collider.gameObject.tag == "Player") {
                    Vector2 offset = item.collider.transform.position - tf.position;
                    if (offset.y > 0f)
                        return true;
                }
            }
            return false;
        }

        public ETouchType GetTouchGround (out bool IsUnder) {
            ETouchType type = ETouchType.NONE;
            UE.RaycastHit2D result = UP2.Raycast (tf.position, Vector2.down, yUnit, 1 << GroundLayer);
            IsUnder = result.collider != null;
            List<ContactPoint2D> point2Ds = new List<ContactPoint2D> ( );
            Collider.GetContacts (point2Ds);
            foreach (ContactPoint2D item in point2Ds) {
                if (item.collider.gameObject.layer == GroundLayer) {
                    Vector2 offset = (item.point - (Vector2)tf.position).normalized;
                    if (offset.x != 0f) {
                        type = offset.x > 0f?ETouchType.RIGHT : ETouchType.LEFT;
                        return type;
                    }
                }
            }
            return type;
        }
        public bool GetTouchGround ( ) {
            List<Collider2D> colliders = new List<Collider2D> ( );
            Collider.OverlapCollider (new ContactFilter2D ( ), colliders);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
                    return true;
                }
            }
            return false;
        }

    }

    [System.Serializable]
    public class RedAirStingrayProps {
        [Header ("Move")]
        public float MoveSpeed = 0f;
        public float MoveRange = 0f;
        [Header ("RandMove")]
        public float RandSpeed = 0f;
        public Vector2 RandRange = Vector2.zero;
    }
    public class ARedAirStingrayComponent : AEnemyComponent {
        public RedAirStingray Parent { get { return this.parent as RedAirStingray; } protected set { this.parent = value; } }
    }
    public enum ETouchType {
        LEFT,
        RIGHT,
        TOUCHED,
        NONE,
    }
}
