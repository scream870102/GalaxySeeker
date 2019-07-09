﻿using Eccentric.UnityUtils;

using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : SingletonMonoBehavior<GameManager> {
    //field to store currentScene
    string currentScene;
    [SerializeField]
    GlobalProps globalProps;
    /// <summary>property of global Property READONLY</summary>
    public GlobalProps G_Props { get { return globalProps; } }
    //ref for active Player
    public Player Player { get { return player; } }
    //when Game started which scene will be Load first
    public string InitScene;
    //field for dialogueManager
    DialogueManager dialogueManager;
    /// <summary>Property for dialogueManager READONLY</summary>
    public DialogueManager DialogueManager { get { return dialogueManager; } }
    //field for active Player
    Player player;
    protected override void Awake ( ) {
        base.Awake ( );
        FindPlayer ( );
        SetScene (InitScene);
        dialogueManager = GetComponent<DialogueManager> ( );
    }

    void Update ( ) {
        //if player doesn't exist try to find it
        if (!player)
            FindPlayer ( );
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
    void FindPlayer ( ) {
        GameObject tmp = GameObject.Find ("Player");
        if (tmp)
            player = tmp.GetComponent<Player> ( );
    }
}
