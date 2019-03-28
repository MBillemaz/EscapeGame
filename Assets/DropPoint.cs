using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DropPoint : MonoBehaviour
{
    public Transform snapTo;
    private Rigidbody body;
    public float snapTime = 2;

    private float dropTimer;
    private Interactable interactable;

    private Logger logger;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Hand used = null;
        if (interactable != null)
            used = interactable.attachedToHand;
        
        if (used)
        {
            //body.useGravity = false;
            body.isKinematic = false;
            dropTimer = -1;

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
           // body.transform.position = used.transform.position - new Vector3(0, 0.2f, 0);
            if (body.useGravity)
                body.AddForce(-Physics.gravity);
        }
        else
        {
           // body.useGravity = true;
            dropTimer += Time.deltaTime / (snapTime / 2);

            body.isKinematic = dropTimer > 1;
            if(Vector3.Distance(transform.position, snapTo.position) < 0.3)
            {
                if (dropTimer > 1)
                {
                    //transform.parent = snapTo;
                    transform.position = snapTo.position;
                    transform.rotation = snapTo.rotation;

                    var sceneChanger = GetComponent<ChangeSceneOnDrop>();
                    if(sceneChanger != null)
                    {
                        sceneChanger.CheckItem();
                    }
                }
                else
                {
                    float t = Mathf.Pow(35, dropTimer);

                    body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, Time.fixedDeltaTime * 4);
                    if (body.useGravity)
                        body.AddForce(-Physics.gravity);

                    transform.position = Vector3.Lerp(transform.position, snapTo.position, Time.fixedDeltaTime * t * 3);
                    transform.rotation = Quaternion.Slerp(transform.rotation, snapTo.rotation, Time.fixedDeltaTime * t * 2);
                }
            }
        }
    }
}
    
