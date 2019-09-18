using UnityEngine;
public class TestEnemy : AEnemy {
    void Awake ( ) {
        Debug.Log ("Awake");
        Init ( );
    }
    void Update ( ) {
        if (Input.GetKeyDown (KeyCode.A)) {
            this.Animator.SetBool ("Hey", true);
        }
        if (Input.GetKeyDown (KeyCode.S)) {
            this.Animator.SetBool ("Hey", false);
        }
    }
    override protected void Dead ( ) {
        Debug.Log ("Hey I am test I am dead");
    }
    public void Cry(){
        Debug.Log("Cryyying");
    }
}
