using Eccentric.Utils;

using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : TSingletonMonoBehavior<GameManager> {
    //field to store currentScene
    string currentScene = "";
    [SerializeField] GlobalProps globalProps = new GlobalProps ( );
    /// <summary>property of global Property READONLY</summary>
    public GlobalProps G_Props => globalProps;
    //ref for active Player
    public Player Player {
        get {
            if (player == null)
                FindPlayer ( );
            return player;
        }
    }
    //when Game started which scene will be Load first
    public EScene InitScene = EScene.LAITER;
    DialogueManager dialogueManager = null;
    /// <summary>Property for dialogueManager READONLY</summary>
    public DialogueManager DialogueManager => dialogueManager;
    Player player = null;
    protected override void Awake ( ) {
        base.Awake ( );
        FindPlayer ( );
        SetScene (InitScene);
        dialogueManager = GetComponent<DialogueManager> ( );
        //uiManager.Init ( );
    }
    void Start ( ) { }
    void Update ( ) { }

    /// <summary>Call this method to load Other scene</summary>
    /// <remarks>make sure scene is in buildSetting</remarks>
    public void SetScene (EScene sceneName, bool IsForceLoad = false) {
        if (!IsForceLoad && SceneManager.GetActiveScene ( ).buildIndex != (int)sceneName) {
            SceneManager.LoadScene ((int)sceneName);
            this.currentScene = sceneName.ToString ( );
        }
        else if (IsForceLoad) {
            SceneManager.LoadScene ((int)sceneName);
            this.currentScene = sceneName.ToString ( );
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

    void Init ( ) {
        if (player == null)
            FindPlayer ( );
        if (dialogueManager == null)
            dialogueManager = GetComponent<DialogueManager> ( );
    }
}
public enum EScene {
    START,
    SPACE_SHIP,
    LAITER,
}
