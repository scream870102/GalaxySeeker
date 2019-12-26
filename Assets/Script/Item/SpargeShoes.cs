using Eccentric.Utils;

using UnityEngine;
namespace GalaxySeeker.Item {
    public class SpargeShoes : Item {
        [SerializeField] int spargingForce;
        [SerializeField] float coolDown;
        [SerializeField] float particleYOffset = 0f;
        bool bCanUse = false;

        ParticleSystem ptc = null;
        ParticleSystem.ShapeModule ptcShape;
        override protected void UsingItem ( ) {
            if (Input.GetButtonDown ("Use") && bCanUse) {
                BeginUsing ( );
                Owner.Velocity = new Vector2 (0f, 0f);
                Owner.AddForce (new Vector2 (Owner.IsFacingRight?spargingForce: -spargingForce, 0f));
                bCanUse = false;
                timer.Reset ( );
                ptcShape.position = new Vector3 (0f, Owner.IsFacingRight? - particleYOffset : particleYOffset, 0f);
                ptc.Play ( );
                Invoke ("StopPTC", 0.5f);
            }
            if (!bCanUse && timer.IsFinished) {
                bCanUse = true;
                AlreadyUsed ( );
            }
        }
        override protected void Reset ( ) { }
        override protected void Init ( ) {
            this.type = EItemType.SHOES;
            SpriteRenderer.enabled = true;
            bCanUse = true;
            ptc = GetComponent<ParticleSystem> ( );
            timer = new Timer (coolDown);
            ptcShape = ptc.shape;
        }
        //Invoke stop ptc after .5f
        void StopPTC ( ) {
            ptc.Stop ( );
        }
    }
}
