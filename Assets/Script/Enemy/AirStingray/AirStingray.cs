using UnityEngine;
/// <summary>Enemy:CannibalFlower</summary>
public class AirStingray : Enemy {
    //---------ref
    //store all values which child component need
    [SerializeField]
    AirStingrayProps props;

    //--------field
    AirStingrayMovement movement = null;
    protected override void Init ( ) {
        base.Init ( );
        movement = new AirStingrayMovement (this, props.MoveSpeed, props.MoveRange, props.sinkSpeed, props.TargetLayer, props.collider);
    }

    protected override void Dead ( ) {
        Debug.Log ("Whyyyyyyyyyyyy " + name + " I am dead");
        this.IsEnable = false;
    }

    [System.Serializable]
    struct AirStingrayProps {
        [Header ("Common Property")]
        public LayerMask TargetLayer;

        [Header ("Move Property")]
        public float MoveSpeed;
        public float MoveRange;
        /// <summary>how fast will stingray sink when player stand on it</summary>
        public float sinkSpeed;
        /// <summary>the collider itself</summary>
        public Collider2D collider;
    }

}
