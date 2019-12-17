﻿using Eccentric.Utils;

using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : TSingletonMonoBehavior<GameManager> {
    //field to store currentScene
    string currentScene = "";
    [SerializeField] GlobalProps globalProps = new GlobalProps ( );
    /// <summary>property of global Property READONLY</summary>
    public GlobalProps G_Props => globalProps;
    //ref for active Player
    public Player Player => player;
    //when Game started which scene will be Load first
    public string InitScene = "";
    //field for dialogueManager
    DialogueManager dialogueManager = null;
    /// <summary>Property for dialogueManager READONLY</summary>
    public DialogueManager DialogueManager { get { return dialogueManager; } }

    [SerializeField] UIManager uiManager = null;
    public UIManager UIManager { get { return uiManager; } }
    //field for active Player
    Player player = null;
    protected override void Awake ( ) {
        base.Awake ( );
        FindPlayer ( );
        SetScene (InitScene);
        dialogueManager = GetComponent<DialogueManager> ( );
        uiManager.Init ( );

    }
    void Start ( ) { }

    void Update ( ) {

    }

    /// <summary>Call this method to load Other scene</summary>
    /// <remarks>make sure scene is in buildSetting</remarks>
    public void SetScene (string sceneName) {
        if (SceneManager.GetActiveScene ( ).name != sceneName) {
            SceneManager.LoadScene (sceneName);
            this.currentScene = sceneName;
        }

    }

    //method for find Player 
    public bool FindPlayer ( ) {
        GameObject tmp = GameObject.Find ("Player");
        if (tmp) {
            player = tmp.GetComponent<Player> ( );
            return true;
        }
        return false;
    }
}
