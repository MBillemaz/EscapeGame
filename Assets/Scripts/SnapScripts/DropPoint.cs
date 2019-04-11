using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DropPoint : MonoBehaviour
{
    public Transform[] snapTo;
    private Rigidbody body;
    public float snapTime = 2;
    public float snapDistance = 0.3f;

    private SnapActionInterface dropPoint;

    private float dropTimer;
    private Interactable interactable;

    private Logger logger;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        body = GetComponent<Rigidbody>();
        dropPoint = null;
    }

    private void FixedUpdate()
    {
        Hand used = null;
        if (interactable != null)
            used = interactable.attachedToHand;
        
        if (used)
        {
            //body.useGravity = false;
            if(dropPoint != null)
            {
                dropPoint.ToggleHasItem();
                dropPoint = null;
            }
            body.isKinematic = false;
            dropTimer = -1;

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
           // body.position = used.transform.position;
           // body.transform.position = used.transform.position - new Vector3(0, 0.2f, 0);
            if (body.useGravity)
                body.AddForce(-Physics.gravity);
        }
        else
        {
           // body.useGravity = true;
            dropTimer += Time.deltaTime / (snapTime / 2);
            
            if(snapTo.Length > 1)
            {
                Array.Sort(snapTo, (x, y) => Vector3.Distance(transform.position, x.position) < Vector3.Distance(transform.position, y.position) ? -1 : 1);
            }
            Transform nearest = snapTo[0];

            Debug.Log(gameObject.name + " " + Vector3.Distance(transform.position, nearest.position));
            if (nearest && Vector3.Distance(transform.position, nearest.position) < snapDistance)
            {
                var drop = nearest.GetComponent<SnapActionInterface>();
                if (drop != null && (!drop.HasItem() || drop.DropObject() == gameObject))
                {
                    dropPoint = drop;
                    if (!drop.HasItem())
                    {
                        drop.ToggleHasItem();
                        drop.setDropObject(gameObject);
                    }

                    if (dropTimer > 1)
                    {
                        //transform.parent = snapTo;
                        transform.position = nearest.position;
                        transform.rotation = nearest.rotation;

                        body.isKinematic = dropTimer > 1;
                        drop.SnapAction(gameObject.name);

                    }
                    else
                    {
                        float t = Mathf.Pow(35, dropTimer);

                        body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, Time.fixedDeltaTime * 4);
                        if (body.useGravity)
                            body.AddForce(-Physics.gravity);

                        transform.position = Vector3.Lerp(transform.position, nearest.position, Time.fixedDeltaTime * t * 3);
                        transform.rotation = Quaternion.Slerp(transform.rotation, nearest.rotation, Time.fixedDeltaTime * t * 2);
                    }
                }
               
            }
        }
    }
}
    
