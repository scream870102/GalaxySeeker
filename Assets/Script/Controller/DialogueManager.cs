using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    [SerializeField] GameObject dialoguePanel = null;
    [SerializeField] Text nameText = null;
    [SerializeField] Text dialogueText = null;
    [SerializeField] RectTransform dialogueUI = null;
    [SerializeField] Vector2 offset = Vector2.zero; //the offset of dialogue panel UI in screen 
    Camera mainCam = null;
    //sentences to show
    Queue<string> sentences = new Queue<string> ( );
    bool bTalking = false;
    bool bFirstSentenceFin = false;
    /// <summary>Property If player is talking to npc right now READONLY</summary>
    public bool IsTalking { get { return bTalking; } }
    /// <summary>Event will invoke when dialogue finish</summary>
    public event System.Action OnDialogueFinish = null;

    void Awake ( ) {
        Init ( );
        sentences = new Queue<string> ( );
        mainCam = Camera.main;
    }

    void Update ( ) {
        if (bTalking) {
            if (Input.GetButtonUp ("Interact")) {
                bFirstSentenceFin = true;
            }
            //if player talking right now and press interact button show next sentence
            if (bFirstSentenceFin && Input.GetButtonDown ("Interact")) {
                DisplayNextSentence ( );
            }
        }

    }

    /// <summary>Other class will call this method to start dialogue</summary>
    public void StartDialogue (Dialogue dialogue, Vector2 pos) {
        //Open DialoguePanel
        dialoguePanel.SetActive (true);
        //Set panel position
        Vector2 screenPos = mainCam.WorldToScreenPoint (pos);
        //offset must calc with screen size
        dialogueUI.anchoredPosition = screenPos + offset;
        bTalking = true;
        nameText.text = dialogue.name;
        sentences.Clear ( );
        //get all sentence from dialogue
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue (sentence);
        }
        //display first sentence
        DisplayNextSentence ( );

    }

    //Show next sentence in queue
    public void DisplayNextSentence ( ) {
        //if queue is empty call EndDialogue
        if (sentences.Count == 0) {
            EndDialogue ( );
            return;
        }
        string sentence = sentences.Dequeue ( );
        StopAllCoroutines ( );
        StartCoroutine (TypeSentence (sentence));
    }

    //easy animation for typing
    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray ( )) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    //when dialogue end init value and call OnDialogueFinish also disable panel
    void EndDialogue ( ) {
        if (OnDialogueFinish != null)
            OnDialogueFinish ( );
        Init ( );
    }

    //turn off panel and init bTalking and bFirstSentenceFin
    void Init ( ) {
        dialoguePanel.SetActive (false);
        bTalking = false;
        bFirstSentenceFin = false;
    }

}
