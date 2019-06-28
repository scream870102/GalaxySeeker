using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Singleton
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    void Awake ( ) {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (this);
            name = "GameManager";
        }
        Init ( );
    }
    #endregion Singleton
    //field to store currentScene
    string currentScene;
    //ref for active Player
    public Player Player { get { return player; } }
    //when Game started which scene will be Load first
    public string InitScene;
    //field for active Player
    Player player;
    void Init ( ) {
        FindPlayer ( );
        SetScene (InitScene);
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
