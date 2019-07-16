using System.Collections.Generic;

using Eccentric.UnityUtils.Attack;

using UnityEngine;
/// <summary>Boss:KingCannibalFlower is the boss on Laiter</summary>
public class KingCannibalFlower : Enemy {
    //------------------ref
    [SerializeField]
    KingCannibalFlowerProps props;
    //----------------field
    KingCannibalFlowerAttack attack = null;
    Animation anim;
    //---------------property
    public Animation Anim { get { return anim; } }
    //---------------event
    public event System.Action<AnimationClip> OnAnimationFinished;

    protected override void Init ( ) {
        base.Init ( );
        attack = new KingCannibalFlowerAttack (this, props.TargetLayer, props.NormalProbability, props.SpecialProbability, props.VineValue, props.VineScratchValue, props.BiteValue, props.Vines);
        anim = GetComponent<Animation> ( );
    }
    void Start ( ) { }

    protected override void Dead ( ) {
        Debug.Log ("Unbelievable" + name + "I actually dead");
        this.IsEnable = false;
    }

    void AnimationFinished (AnimationClip anim) {
        if (OnAnimationFinished != null)
            OnAnimationFinished (anim);
    }

    [System.Serializable]
    struct KingCannibalFlowerProps {
        [Header ("Common Property")]
        public LayerMask TargetLayer;
        [Header ("Attack Property")]
        public AttackValue VineValue;
        public AttackValue VineScratchValue;
        public AttackValueRadius BiteValue;
        public List<Collider2D> Vines;
        [Range (0f, 1f)]
        public float NormalProbability;
        [Range (0f, 1f)]
        public float SpecialProbability;

    }
}
