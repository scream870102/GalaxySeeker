using Eccentric.Collections;
using Eccentric.Utils;

using UnityEngine;
public class PlayerShooting : PlayerComponent {
    [SerializeField] Transform shootTf;
    //use object pool to manage bullet
    [SerializeField] ObjectPool Bullets = null;
    //if player can shoot right now
    bool bShootAble;
    // field for current bullets in clip
    int clipCapacity;
    // is now in reloading animation
    bool bReloading;
    Timer timer;
    //spawn all bullets
    void Awake ( ) {
        Bullets.Init ( );
        bShootAble = true;
        clipCapacity = Parent.Props.MaxClipCapacity;
        bReloading = false;
        timer = new Timer (Parent.Props.CoolDown);
    }

    //if player hit shoot button shoot bullet 
    protected override void Tick ( ) {
        //if timer say u can shoot and u also not in reloadAnimation then u can shoot bullet
        if (Input.GetButtonDown ("Shoot") && bShootAble && !bReloading) {
            if (clipCapacity <= 0) {
                Reload ( );
                return;
            }
            Bullet bullet = Bullets.GetPooledObject<int> (0) as Bullet;
            clipCapacity--;
            bullet.Fire (Parent.IsFacingRight?Vector2.right : Vector2.left, shootTf.position, Parent.Props.Damage);
            timer.Reset ( );
            bShootAble = false;
            Parent.Anim.SetTrigger ("Attack");
        }
        bShootAble = timer.IsFinished;
    }

    //method when current bullet is under or equal to zero reload clip
    void Reload ( ) {
        bReloading = true;
        Parent.Anim.SetTrigger ("Reload");
        Invoke ("ReloadFinish", Parent.Props.ReloadTime);
    }

    //method when reload is finish
    void ReloadFinish ( ) {
        bReloading = false;
        clipCapacity = Parent.Props.MaxClipCapacity;
    }
}
