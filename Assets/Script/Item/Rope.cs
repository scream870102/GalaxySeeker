using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Rope : Item {
    public float ropeMaxCastDistance;
    public LayerMask ropeLayerMask;
    DistanceJoint2D distanceJoint;
    LineRenderer lineRenderer;
    bool bUsingRope;

    override protected void UsingItem ( ) {
        if (Input.GetButtonDown ("Use") && !bUsingRope) {
            RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, ropeMaxCastDistance, ropeLayerMask);
            if (hit.collider != null) {
                bUsingRope = true;
                owner.AddForce (new Vector2 (0f, 2f));
                owner.IsPlayerSwing = true;
                distanceJoint.connectedAnchor = hit.point;
                owner.HookPoint = hit.point;
                distanceJoint.distance = Vector2.Distance (transform.position, hit.point);
                distanceJoint.enabled = true;
                lineRenderer.SetPosition (0, hit.point);
                lineRenderer.SetPosition (1, transform.position);
                lineRenderer.enabled = true;

            }
        }
        else if (Input.GetButtonDown ("Use") && bUsingRope) {
            AlreadUsed ( );
        }
        else if (bUsingRope) {
            lineRenderer.SetPosition (1, transform.position);
        }

    }
    override protected void Reset ( ) {
        bUsingRope = false;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        owner.IsPlayerSwing = false;
        owner.HookPoint = Vector2.zero;
    }

    override protected void Init ( ) {
        base.Init ( );
        distanceJoint = inventory.Parent.gameObject.AddComponent<DistanceJoint2D> ( );
        lineRenderer = GetComponent<LineRenderer> ( );
        distanceJoint.autoConfigureDistance = false;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        bUsingRope = false;
    }
    override protected void RemoveItem ( ) {

    }
}
