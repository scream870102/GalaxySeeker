using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestCollision : MonoBehaviour {
    public Collider2D col;
    // Start is called before the first frame update
    void Start ( ) {
        col = GetComponent<Collider2D> ( );

    }

    // Update is called once per frame
    void Update ( ) {
        List<ContactPoint2D> point2Ds = new List<ContactPoint2D> ( );
        col.GetContacts (point2Ds);
        foreach (ContactPoint2D item in point2Ds) {
            Debug.Log (item.normal);
        }
    }

}
