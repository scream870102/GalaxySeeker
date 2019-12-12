using System.Collections.Generic;

using GalaxySeeker.Move;

using UnityEngine;
using UE = UnityEngine;
using UP2 = UnityEngine.Physics2D;
namespace GalaxySeeker.Enemy.AirStingray {
    public class AirStingray : AEnemy {
        [SerializeField] AirStingrayProps props;
        [SerializeField] int sinkMoveIndex = 0;
        PingPongMove horiMove = null;
        bool bTouchedPlayer = false;
        int groundLayer = 0;
        Vector2 initPos = Vector2.zero;
        new Collider2D collider = null;
        float yUnit = 0f;
        public Vector2 InitPos => initPos;
        public PingPongMove HoriMove { get => horiMove; set { if (horiMove == null)horiMove = value; } }
        //public PingPongMove HoriMove { get { return horiMove; } set { if (horiMove == null)horiMove = value; } }
        public Collider2D Collider => collider;
        public bool IsTouchedByPlayer { set { if (value != bTouchedPlayer) { bTouchedPlayer = value; ChooseNextAction ( ); } } get => bTouchedPlayer; }
        public AirStingrayProps Props => props;
        void Awake ( ) {
            Init ( );
            collider = GetComponent<Collider2D> ( );
            initPos = tf.position;
            groundLayer = LayerMask.NameToLayer ("Ground");
            yUnit = (collider as BoxCollider2D).size.y * tf.localScale.y * 0.5f;
            Debug.Log (yUnit);
        }

        override protected void Dead ( ) {
            Debug.Log ("I am Airstingray I come from hell");
            this.gameObject.SetActive (false);
        }
        override public void ChooseNextAction ( ) {
            //if touched by player use skinAction
            if (IsTouchedByPlayer) {
                this.Animator.SetTrigger (this.Actions [sinkMoveIndex].Trigger);
            }
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
            UE.RaycastHit2D result = UP2.Raycast (tf.position, Vector2.down, yUnit, 1 << groundLayer);
            IsUnder = result.collider != null;
            List<ContactPoint2D> point2Ds = new List<ContactPoint2D> ( );
            Collider.GetContacts (point2Ds);
            foreach (ContactPoint2D item in point2Ds) {
                if (item.collider.gameObject.layer == groundLayer) {
                    Vector2 offset = (item.point - (Vector2)tf.position).normalized;
                    if (offset.x != 0f) {
                        type = offset.x > 0f?ETouchType.RIGHT : ETouchType.LEFT;
                        return type;
                    }
                }
            }
            return type;
        }
    }

    [System.Serializable]
    public class AirStingrayProps {
        [Header ("Move")]
        public float speed = 2.0f;
        public float range = 1.0f;
        [Header ("SinkMove")]
        public float sinkSpeed = 0.2f;
    }

    public class AAirStingrayComponent : AEnemyComponent {
        public AirStingray Parent { get { return this.parent as AirStingray; } protected set { this.parent = value; } }
    }
    public enum ETouchType {
        LEFT,
        RIGHT,
        NONE,
    }
}
