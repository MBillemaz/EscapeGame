using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RaycastInput : MonoBehaviour {

    public SteamVR_Action_Boolean action;
    private GameObject rayCastObject;
    private Hand hand;

    void Start () {
        hand = GetComponent<Hand>();
        action.AddOnChangeListener(OnTriggerPressed, hand.handType);
        
	}

    private void Update()
    {
        RaycastHit rayHit;
        //Check if raycast hits anything
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red, 1);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit))
        {
           
            string tag = rayHit.transform.tag;
            this.rayCastObject = rayHit.transform.gameObject;

        }
    }

    private void OnTriggerPressed(SteamVR_Action_In actionIn)
    {
        Debug.Log(actionIn.name);
        RaycastEvent hitObject = rayCastObject.GetComponent<RaycastEvent>();
        if (hitObject)
        {
            Debug.Log(rayCastObject.name);
            hitObject.Test();
        }
    }

    void OnDestroy()
    {
        action.RemoveOnChangeListener(OnTriggerPressed, hand.handType);
    }

}
