using UnityEngine;
public class TestEnemyComponent : GalaxySeeker.Enemy.AEnemyComponent {
    public TestEnemy Parent { get { return this.parent as TestEnemy; } protected set { this.parent = value; } }
    public int testValue = 1;
    void OnEnable ( ) { }

    void OnDisable ( ) {
        Debug.Log ("OnDisable");
        Parent.OnTriggerStay -= TriggerStay;
    }
    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!Parent) {
            Debug.Log ("Find Parent");
            Parent = animator.GetComponent<TestEnemy> ( );
            Parent.OnTriggerStay += TriggerStay;
            this.testValue = 2;
        }
        Debug.Log ("StateEnter");
    }
    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log ("StateExit");
        Parent.Cry ( );
    }
    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.testValue++;
        Debug.Log ("Hey::" + testValue);
    }
    void TriggerStay (Collider2D col) {
        Debug.Log (col.name);
    }
}
