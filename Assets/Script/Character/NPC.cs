using UnityEngine;
/// <summary>class of NPC its basic ability is to conversation with player</summary>
/// <remarks>can have dialogue if Need</remarks>
[RequireComponent (typeof (Collider2D))]
public class NPC : Character {
    //Dialogue which will interact with player
    [SerializeField] protected Dialogue dialogue;
    public event System.Action<string, NPC> OnNPCDialogueFinish;
    //if player stay in trigger can interact with player
    void OnTriggerStay2D (Collider2D other) {
        if (dialogue.sentences.GetLength (0) > 0)
            Conversation (other);
    }

    //if player press interact send dialogue to DialogueManager
    protected virtual void Conversation (Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (Input.GetButtonDown ("Interact") && !GameManager.Instance.DialogueManager.IsTalking) {
                //set name to this character
                dialogue.name = this.name;
                GameManager.Instance.DialogueManager.StartDialogue (dialogue, transform.position);
                GameManager.Instance.DialogueManager.OnDialogueFinish += DialogueFinish;
            }
        }
    }

    /// <summary>Call back method when dialogue finish CAN OVERRIDE</summary>
    /// <remarks>MUST Remove this method From DialogueManager.OnDialogueFinish when method end</remarks>
    /// <example><code>GameManager.Instance.DialogueManager.OnDialogueFinish -= DialogueFinish;</code></example>
    protected virtual void DialogueFinish ( ) {
        GameManager.Instance.DialogueManager.OnDialogueFinish -= DialogueFinish;
        if (OnNPCDialogueFinish != null) {
            OnNPCDialogueFinish (this.name, this);
        }
    }
}
