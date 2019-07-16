using UnityEngine;
/// <summary>Enemy:RedAirStingray</summary>
/// <remarks>will get in rage when player stand on it</remarks>
public class RedAirStingray : Enemy {
    //---------ref
    //store all values which child component need
    [SerializeField]
    RedAirStingrayProps props;

    //--------field
    RedAirStingrayMovement movement = null;
    protected override void Init ( ) {
        base.Init ( );
        movement = new RedAirStingrayMovement (this, props.PingPongVelocity,props.RangeVelocity, props.MoveRange, props.sinkRaiseSpeed, props.TargetLayer, props.collider);
    }

    protected override void Dead ( ) {
        Debug.Log ("Whyyyyyyyyyyyy " + name + " I am dead");
        this.IsEnable = false;
    }

    [System.Serializable]
    struct RedAirStingrayProps {
        [Header ("Common Property")]
        public LayerMask TargetLayer;

        [Header ("Move Property")]
        /// <summary>the origin velocity of redAirstingray</summary>
        public float PingPongVelocity;
        /// <summary>when player stand on it it will raise its velocity to this one</summary>
        public float RangeVelocity;
        public Vector2 MoveRange;
        /// <summary>how fast will stingray sink when player stand on it</summary>
        public float sinkRaiseSpeed;
        /// <summary>the collider itself</summary>
        public Collider2D collider;

    }

}
