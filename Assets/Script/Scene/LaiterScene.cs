using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
public class LaiterScene : Scene {
    // the global gravity of Laiter Planet
    [SerializeField]
    Vector2 gravity;
    // the key point npc should react with
    [SerializeField]
    List<NPC> npcS;
    // the wall will disappera after  all key point finish
    [SerializeField]
    GameObject wallBeforeBossFight = null;
    // ref for boss
    [SerializeField]
    Enemy boss = null;
    // the elements should finish in this planet 
    List<bool> keyPoints = new List<bool> ( );
    // if all key point finish then can enter the boss fight
    bool bAllKeyNPCFin = false;

    //change the planet gravity
    void Awake ( ) {
        Physics2D.gravity = gravity;
    }
    // Start is called before the first frame update
    void Start ( ) {
        foreach (NPC npc in npcS)
            npc.OnNPCDialogueFinish += KeyNpcDialogueFinish;
        for (int i = 0; i < npcS.Count; i++)
            keyPoints.Add (false);
        if (boss) boss.Stats.OnHealthReachedZero += BossDead;
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
        wallBeforeBossFight.SetActive (false);
    }

    //callback method when boss dead
    //set global gravity to default back
    void BossDead ( ) {
        Debug.Log ("Hey I am the boss and I am dead");
        Physics2D.gravity = GameManager.Instance.G_Props.DefaultGravity;
        #region  Test
        GameManager.Instance.SetScene ("Start");
        #endregion Test

    }

}
