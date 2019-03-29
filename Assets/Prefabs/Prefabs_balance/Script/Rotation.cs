using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    private Rigidbody body;
    private Quaternion currentRotation;
    public Transform originalPosition;
    private Vector3 originalRot;
    private float dropTimer = 0;
    private bool tabAppear = false;
    // Inistialisation de la barre
    void Start()
    {
        // Gestion du Rigiboby de la Barre de la balance
        this.body = GetComponent<Rigidbody>();

        // Récuperation et detection des socles 
        GameObject Sce1 = GameObject.Find("Socle 1");
        Socle_1 Sce1Script = Sce1.GetComponent<Socle_1>();

        GameObject Sce2 = GameObject.Find("Socle 2");
        Socle_2 Sce2Script = Sce2.GetComponent<Socle_2>();
        this.body.constraints = RigidbodyConstraints.FreezeRotation;

    }

    void Update()
    {

        // Récuperation et detection des socles 
        GameObject Sce1 = GameObject.Find("Socle 1");
        Socle_1 Sce1Script = Sce1.GetComponent<Socle_1>();

        GameObject Sce2 = GameObject.Find("Socle 2");
        Socle_2 Sce2Script = Sce2.GetComponent<Socle_2>();


        if (Sce1Script.TotalForce != 10 || Sce2Script.TotalForce != 10 || Sce1Script.TotalForce != Sce2Script.TotalForce)
        {
            this.body.constraints = RigidbodyConstraints.None;

        }
        if (Sce1Script.TotalForce == Sce2Script.TotalForce)
        {
            if(body.rotation.x > -0.01 && body.rotation.x < 0.01)
            {
                body.constraints = RigidbodyConstraints.FreezeRotationX;
                if (!tabAppear)
                {
                    tabAppear = !tabAppear;
                    /**
                     * SPAWN TABLETTE 
                     */

                }
            } else
            {
                this.body.angularVelocity = body.rotation.x > 0 ? new Vector3(-1, 0, 0) : body.rotation.x < 0 ? new Vector3(0.5f, 0, 0) : Vector3.zero;
            }
           
        } else
        {
            dropTimer = 0;
        }
    }
}
