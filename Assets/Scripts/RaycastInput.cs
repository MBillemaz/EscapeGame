using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RaycastInput : MonoBehaviour {

    public SteamVR_Action_Boolean action;
    private GameObject rayCastObject;
    private Hand hand;
    public GameObject viseur;
    private Vector3 initialViseurPos;
    void Start () {
        hand = GetComponent<Hand>();
        action.AddOnChangeListener(OnTriggerPressed, hand.handType);
        initialViseurPos = viseur.transform.position;
	}

    private void Update()
    {
        RaycastHit rayHit;
        //Check if raycast hits anything
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red, 1);
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, 2, 1 << 14))
        {
           
            string tag = rayHit.transform.tag;
            rayCastObject = rayHit.transform.gameObject;
            viseur.transform.position = rayHit.point;
        } else
        {
            viseur.transform.position = initialViseurPos;
        }
    }

    private void OnTriggerPressed(SteamVR_Action_In actionIn)
    {
        Debug.Log(actionIn.name);
        if(rayCastObject)
        {
            RaycastEvent hitObject = rayCastObject.GetComponent<RaycastEvent>();
            if (hitObject)
            {
                Debug.Log(rayCastObject.name);
                hitObject.Test();
            }
        }
       
    }

    void OnDestroy()
    {
        action.RemoveOnChangeListener(OnTriggerPressed, hand.handType);
    }

}
