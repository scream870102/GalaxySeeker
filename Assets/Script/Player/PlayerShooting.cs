using Eccentric.UnityUtils;
using Eccentric.UnityUtils.Collections;

using UnityEngine;
public class PlayerShooting : PlayerComponent {
    //use object pool to manage bullet
    [SerializeField]
    ObjectPool Bullets;
    public float reloadTime;
    /// <summary>the time between two shooting action</summary>
    public float coolDown;
    //if player can shoot right now
    bool bShootable;
    /// <summary>how many bullets can weapon take</summary>
    public int maxClipCapacity;
    // field for current bullets in clip
    int clipCapacity;
    // is now in reloading animation
    bool bReloading;
    CountdownTimer timer;
    //spawn all bullets
    void Awake ( ) {
        Bullets.Init ( );
        bShootable = true;
        clipCapacity = maxClipCapacity;
        bReloading = false;
        timer = new CountdownTimer (coolDown);
    }

    //if player hit shoot button shoot bullet 
    protected override void Tick ( ) {
        //if timer say u can shoot and u also not in reloadAnimation then u can shoot bullet
        if (Input.GetButtonDown ("Shoot") && bShootable && !bReloading) {
            if (clipCapacity <= 0) {
                Reload ( );
                return;
            }
            Bullet bullet = Bullets.GetPooledObject ( ) as Bullet;
            clipCapacity--;
            bullet.Fire (Parent.IsFacingRight?Vector2.right : Vector2.left, transform.position, Parent.Props.Damage);
            timer.Reset ( );
            bShootable = false;
        }

        bShootable = timer.IsFinished;
    }
    //method when current bullet is under or equal to zero reload clip
    void Reload ( ) {
        bReloading = true;
        Invoke ("ReloadFinish", reloadTime);
    }

    //method when reload is finish
    void ReloadFinish ( ) {
        bReloading = false;
        clipCapacity = maxClipCapacity;
    }
}
