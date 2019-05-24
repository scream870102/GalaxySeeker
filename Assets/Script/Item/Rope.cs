using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Rope : Item {
    /// <summary>define how far can rope cast </summary>
    public float ropeMaxCastDistance;
    /// <summary>define what layer should rope react with</summary>
    public LayerMask ropeLayerMask;
    //ref for DistanceJoint2D which is a part of component on the player
    private DistanceJoint2D distanceJoint;
    //ref for LineRenderer
    private LineRenderer lineRenderer;
    //define if player is using rope right now
    private bool bUsing;
    public int swingForce;

    override protected void UsingItem ( ) {
        //if player hit using button then cast rope
        if (Input.GetButtonDown ("Use") && !bUsing) {
            //call beginUsing to tell invetoru player is using rope right now
            BeginUsing ( );
            //hit store the ref of rope cast
            RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, ropeMaxCastDistance, ropeLayerMask);
            //if rope hit something then update it
            if (hit.collider != null) {
                bUsing = true;
                //add a vector force to player prevent player still stay on ground
                owner.AddForce (new Vector2 (0f, 2f));
                //change this boolean make playerMovement change its reaction different from other state
                owner.IsSwing = true;
                //make player know hookpoint then player can update its own normal vector from its position to hook point
                owner.HookPoint = hit.point;
                //connect rope
                distanceJoint.connectedAnchor = hit.point;
                //set distanceJoint2D distance and enable it
                distanceJoint.distance = Vector2.Distance (transform.position, hit.point);
                distanceJoint.enabled = true;
                //set lineRenderer position from player to rope cast point and enable it
                lineRenderer.SetPosition (0, hit.point);
                lineRenderer.SetPosition (1, transform.position);
                lineRenderer.enabled = true;

            }
        }
        //if player hit using button and rope is using now reset rope
        else if (Input.GetButtonDown ("Use") && bUsing) {
            AlreadUsed ( );
        }
        //if rope is now using keep update lineRenderer about player position
        else if (bUsing) {
            lineRenderer.SetPosition (1, transform.position);
        }

    }

    //disable distancJoing on player and lineRenderer
    //tell player is not swinging right now
    override protected void Reset ( ) {
        bUsing = false;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        owner.IsSwing = false;
        owner.HookPoint = Vector2.zero;
    }

    //Add a distanceJoint2D to player and init it
    //disable lineRenderer and distanceJoint2d
    override protected void Init ( ) {
        sr.enabled = false;
        distanceJoint = inventory.Parent.gameObject.AddComponent<DistanceJoint2D> ( );
        lineRenderer = GetComponent<LineRenderer> ( );
        distanceJoint.autoConfigureDistance = false;
        distanceJoint.enableCollision = true;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        bUsing = false;
        owner.Stats.swingForce.baseValue = swingForce;

    }
}
