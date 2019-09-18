using Eccentric.Utils;

using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : TSingletonMonoBehavior<GameManager> {
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

    [SerializeField] UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }
    //field for active Player
    Player player;
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
