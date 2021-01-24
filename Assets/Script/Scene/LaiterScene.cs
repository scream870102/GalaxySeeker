using System.Collections.Generic;

using Cinemachine;

using GalaxySeeker.Enemy;

using UnityEngine;
using UnityEngine.Playables;
public class LaiterScene : Scene {
    // the global gravity of Laiter Planet
    [SerializeField] Vector2 gravity = Vector2.zero;
    // the key point npc should react with
    [SerializeField] List<NPC> npcS = new List<NPC> ( );
    // the wall will disappera after  all key point finish
    [SerializeField] GameObject wallBeforeBossFight = null;
    // ref for boss
    [SerializeField] AEnemy boss = null;
    // the elements should finish in this planet 
    List<bool> keyPoints = new List<bool> ( );
    // if all key point finish then can enter the boss fight
    bool bAllKeyNPCFin = false;
    bool bGameEnd = false;
    [SerializeField] Cinemachine.CinemachineBrain cinCamera = null;
    [SerializeField] PlayableDirector director = null;
    [SerializeField] CinemachineVirtualCamera bossVCam = null;
    [SerializeField] CinemachineVirtualCamera playerVCam = null;
    [SerializeField] PlayerUI playerUI = null;
    [SerializeField] GameEndUI gameEndUI = null;
    [SerializeField] BossUI bossUI = null;
    //change the planet gravity
    void Awake ( ) {
        Physics2D.gravity = gravity;
    }
    // Start is called before the first frame update
    void Start ( ) {
        playerUI.Init ( );
        gameEndUI.Init ( );
        bossUI.Init ( );
        playerUI.EnableHealthUI (true);
        bossUI.Enable = false;
        GameManager.Instance.Player.Stats.OnHealthReachedZero += OnPlayerDead;
        foreach (NPC npc in npcS)
            npc.OnNPCDialogueFinish += KeyNpcDialogueFinish;
        for (int i = 0; i < npcS.Count; i++)
            keyPoints.Add (false);
        if (boss) {
            boss.Stats.OnHealthReachedZero += OnBossDead;
            boss.IsEnable = false;
        }

    }

    void Update ( ) {
        if (bGameEnd && Input.GetButtonDown ("Interact")) {
            GameManager.Instance.SetScene (EScene.LAITER, true);
        }
    }

    //callback method when key npc finish its dialogue
    //if all key dialogue finish call AllKeyNPCEventFin
    void KeyNpcDialogueFinish (string npc, NPC npcRef) {
        int index = npcS.IndexOf (npcRef);
        if (index != -1)
            keyPoints [index] = true;
        bAllKeyNPCFin = !keyPoints.Contains (false);
        if (bAllKeyNPCFin)
            AllKeyNPCEventFin ( );
    }

    //if all key event finish disable the wall before boss fight
    void AllKeyNPCEventFin ( ) {
        director.Play ( );
        wallBeforeBossFight.SetActive (false);
        boss.IsEnable = true;
        bossUI.Enable = true;
        GameManager.Instance.Audio.clip = GameManager.Instance.BossFightClip;
        GameManager.Instance.Audio.Play();
    }

    //callback method when boss dead
    //set global gravity to default back
    void OnBossDead ( ) {
        Debug.Log ("Hey I am the boss and I am dead");
        Physics2D.gravity = GameManager.Instance.G_Props.DefaultGravity;
        bossVCam.gameObject.SetActive (false);
        playerVCam.gameObject.SetActive (true);
        bossUI.Enable = false;
        GameManager.Instance.Audio.clip = GameManager.Instance.NormalClip;
        GameManager.Instance.Audio.Play();
    }

    void OnPlayerDead ( ) {
        gameEndUI.Enable = true;
        bGameEnd = true;
        GameManager.Instance.Player.gameObject.SetActive (false);
        Debug.Log ("GameOver");
    }

}
