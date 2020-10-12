using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ChangeScene : MonoBehaviour {

    public SteamVR_Action_Boolean action;

    public Hand hand;
    void Start () {

        //action.AddOnChangeListener(OnTriggerPressed, hand.handType);
    }

    private void Update()
    {
        if (action.GetState(hand.handType))
        {
            OnTriggerPressed();
        }
    }
    private void OnTriggerPressed()
    {
        Change();
    }

    void Change()
    {

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }

    void OnDestroy()
    {
       // action.RemoveOnChangeListener(OnTriggerPressed, hand.handType);
    }
}
