using System.Collections.Generic;

using GalaxySeeker.Attack;

using UnityEngine;
/// <summary>Boss:KingCannibalFlower is the boss on Laiter</summary>
public class KingCannibalFlower : Enemy {
    //test
    //------------------ref
    [SerializeField]
    KingCannibalFlowerProps props;
    //----------------field
    KingCannibalFlowerAttack attack = null;
    Animation anim;
    //---------------property
    /// <summary>READONLY to get the component animation on this character</summary>
    public Animation Anim { get { return anim; } }
    //---------------event
    /// <summary>event when one animation finished will invoke this event</summary>
    public event System.Action<AnimationClip> OnAnimationFinished;

    protected override void Init ( ) {
        base.Init ( );
        attack = new KingCannibalFlowerAttack (this, props.TargetLayer, props.NormalProbability, props.SpecialProbability, props.VineStingValue, props.VineScratchValue, props.BiteValue, props.VineValue, props.NeedleValue, props.Vines, props.Needles);
        anim = GetComponent<Animation> ( );
    }

    protected override void Dead ( ) {
        Debug.Log ("Unbelievable" + name + "I actually dead");
        this.IsEnable = false;
    }

    /// <summary>Callback method for animation event</summary>
    /// <param name="anim">which animation clip finished</param>
    void AnimationFinished (AnimationClip anim) {
        if (OnAnimationFinished != null)
            OnAnimationFinished (anim);
    }

    [System.Serializable]
    struct KingCannibalFlowerProps {
        [Header ("Common Property")]
        public LayerMask TargetLayer;
        [Header ("Attack Property")]
        public AttackValue VineStingValue;
        public AttackValue VineScratchValue;
        public AttackValueRadius BiteValue;
        public AttackValueRadius NeedleValue;
        public AttackValueRadius VineValue;
        /// <summary>all the collider of vines on KingCannibalFlower</summary>
        public List<Collider2D> Vines;
        public List<Collider2D> Needles;
        [Range (0f, 1f)]
        /// <summary>the probability to use normal attack include bite ,needle,vine</summary>
        public float NormalProbability;
        [Range (0f, 1f)]
        /// <summary>the probability to use special attack include vineScratch and vineSting</summary>
        public float SpecialProbability;

    }
}
