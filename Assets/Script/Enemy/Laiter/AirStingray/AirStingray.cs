using System.Collections.Generic;

using GalaxySeeker.Move;

using UnityEngine;
namespace GalaxySeeker.Enemy.AirStingray {
    public class AirStingray : AEnemy {
        [SerializeField] int sinkMoveIndex = 0;
        PingPongMove horiMove = null;
        bool bTouchedPlayer = false;
        Vector2 initPos = Vector2.zero;
        new Collider2D collider = null;
        public Vector2 InitPos { get { return initPos; } }
        public PingPongMove HoriMove { get { return horiMove; } set { if (horiMove == null) horiMove = value; } }
        public Collider2D Collider { get { return collider; } }
        public bool IsTouchedByPlayer { set { if (value != bTouchedPlayer) { bTouchedPlayer = value; ChooseNextAction ( ); } } get { return bTouchedPlayer; } }
        void Awake ( ) {
            Init ( );
            collider = GetComponent<Collider2D> ( );
            initPos = tf.transform.position;
        }

        override protected void Dead ( ) {
            Debug.Log ("I am Airstingray I come from hell");
        }
        override public void ChooseNextAction ( ) {
            //if touched by player use skinAction
            if (IsTouchedByPlayer) {
                this.Animator.SetTrigger (this.Actions [sinkMoveIndex].Trigger);
            }
        }

        public bool GetTouchPlayer ( ) {
            bool IsTouched = false;
            List<Collider2D> colliders = new List<Collider2D> ( );
            Collider.OverlapCollider (new ContactFilter2D ( ), colliders);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.tag == "Player") {
                    List<ContactPoint2D> point2Ds = new List<ContactPoint2D> ( );
                    collider.GetContacts (point2Ds);
                    foreach (ContactPoint2D item in point2Ds) {
                        if (item.normal.y >= 1f)
                            IsTouched = true;
                    }
                }
            }
            return IsTouched;
        }

        public ETouchType GetTouchGround (out bool IsUnder) {
            ETouchType type = ETouchType.NONE;
            IsUnder = false;
            List<Collider2D> colliders = new List<Collider2D> ( );
            Collider.OverlapCollider (new ContactFilter2D ( ), colliders);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
                    List<ContactPoint2D> point2Ds = new List<ContactPoint2D> ( );
                    collider.GetContacts (point2Ds);
                    foreach (ContactPoint2D item in point2Ds) {
                        if (item.normal.y <= -1f) {
                            IsUnder = true;
                        }
                        if (item.normal.x >= 1f)
                            type = ETouchType.RIGHT;
                        else if (item.normal.x <= -1f)
                            type = ETouchType.LEFT;
                    }
                }
            }
            return type;
        }
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
