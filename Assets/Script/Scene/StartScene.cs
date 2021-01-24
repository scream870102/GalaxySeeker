// TODO:
// FIXME:
// Fix how load the scene
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class StartScene : Scene {
    //field to store selected option index
    int currentIndex = 0;
    //field to store max index of option
    int maxIndex = 0;
    #region Animation
    //text color of actived button
    public Color ActiveColor = Color.white;
    //text color of deactive button
    public Color DeactiveColor = Color.white;
    #endregion Animation
    //field to store all button include Text and Action
    [SerializeField] List<Button> buttons = new List<Button> ( );
    [SerializeField] Canvas controlGroup = null;
    bool bControlPanelOn = false;

    void Start ( ) {
        currentIndex = 0;
        maxIndex = buttons.Count - 1;
        bControlPanelOn = false;
        controlGroup.enabled = false;
    }

    void Update ( ) {
        //Get User Input
        if (Input.GetButtonDown ("Vertical")) {
            Debug.Log("AAA");
            currentIndex -= (int)Input.GetAxisRaw ("Vertical");
            if (currentIndex > maxIndex)
                currentIndex = 0;
            else if (currentIndex < 0)
                currentIndex = maxIndex;

        }
        //Render
        for (int i = 0; i < buttons.Count; i++)
            SetAnimation (buttons [i].text, i == currentIndex? true : false);
        //If player Press Interact invoke button action
        if (Input.GetButtonDown ("Interact")) {
            buttons [currentIndex].action.Invoke ( );
        }
        if (bControlPanelOn && Input.GetButtonDown ("Jump")) {
            controlGroup.enabled = false;
            bControlPanelOn = false;
        }
    }

    //set color of button due to boolean which define this option being selected or not
    void SetAnimation (Text option, bool value) {
        option.color = value?ActiveColor : DeactiveColor;
    }

    /// <summary>action when start button being pressed</summary>
    /// <summary>Will load Scene Spaceship</summary>
    public void StartBtnAction ( ) {
        GameManager.Instance.SetScene (EScene.LAITER);
    }

    /// <summary>action when option button being pressed</summary>
    /// <remarks>NOT DEFINED</remarks>
    public void ControlBtnAction ( ) {
        Debug.Log ("Option being Pressed");
        controlGroup.enabled = true;
        bControlPanelOn = true;
    }

    /// <summary>action when exit button being pressed</summary>
    /// <remarks>will close this application</remarks>
    public void ExitBtnAction ( ) {
        Application.Quit ( );
    }

    //struct for button text and action
    [System.Serializable]
    struct Button {
        public Text text;
        public UnityEvent action;

    }
}
