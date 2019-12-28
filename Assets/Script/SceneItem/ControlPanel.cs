// TODO:
// FIXME:
//  FIX HOW load another scene

using System.Collections.Generic;

using GalaxySeeker;

using UnityEngine;
public class ControlPanel : MonoBehaviour, IInteractable {
    //all UI elements about SELECTION belongs to which canvas
    [SerializeField] Canvas selectCanvas = null;
    //if select panel enable or not
    bool bSelectPanelEnable = false;
    //all planets player can choose
    [SerializeField] List<EScene> planet = new List<EScene> ( );
    //which planet player current choosing
    int currentIndex = 0;
    //max #of planet
    int maxIndex = 0;
    //collider of control panel
    Collider2D col = null;
    //if player choosing planet right now or not
    bool bChoosing = false;
    void Awake ( ) {
        selectCanvas.enabled = false;
        maxIndex = planet.Count - 1;
        col = GetComponent<Collider2D> ( );
        GameManager.Instance.FindPlayer ( );

    }

    void Update ( ) {
        //when select panel enable
        if (bSelectPanelEnable) {
            if (Input.GetButtonUp ("Interact"))
                bChoosing = true;
            //Handle player Input
            //make sure currentIndex is between maxIndex and 0
            if (Input.GetButtonDown ("Horizontal")) {
                currentIndex += (int) Input.GetAxisRaw ("Horizontal");
                if (currentIndex > maxIndex)
                    currentIndex = 0;
                else if (currentIndex < 0)
                    currentIndex = maxIndex;
            }
            //Enter specifc scene due to player chosen
            if (Input.GetButtonDown ("Interact") && bChoosing) {
                GameManager.Instance.SetScene (planet [currentIndex]);
            }
            //if player press switch button turn chosen panel of
            if (Input.GetButtonDown ("Switch") && bChoosing) {
                TurnPanel (false);
            }
        }
    }

    //if player enter trigger and press interact button turn on panel
    void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown ("Interact")) {
            Interact ( );
        }
    }

    public void Interact ( ) {
        TurnPanel (true);
    }

    //when panel on 
    //disable all action of player and disable collider
    void TurnPanel (bool value) {
        bChoosing = false;
        selectCanvas.enabled = value;
        bSelectPanelEnable = value;
        GameManager.Instance.Player.EnableComponents (!value);
        col.enabled = !value;
    }
}
