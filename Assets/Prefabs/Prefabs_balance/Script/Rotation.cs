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
       // this.body.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    }

    void Update()
    {

        // Récuperation et detection des socles 
        GameObject Sce1 = GameObject.Find("Socle 1");
        Socle_1 Sce1Script = Sce1.GetComponent<Socle_1>();

        GameObject Sce2 = GameObject.Find("Socle 2");
        Socle_2 Sce2Script = Sce2.GetComponent<Socle_2>();
        
        if (Sce1Script.TotalForce != 10 && Sce1Script.TotalForce == Sce2Script.TotalForce)
        {
            if(body.rotation.z > -0.01 && body.rotation.z < 0.01)
            {
                body.constraints = RigidbodyConstraints.FreezeAll;
                if (!tabAppear)
                {
                    tabAppear = !tabAppear;
                    GetComponent<GenerateTablette>().Spawn();   

                }
            } else
            {
                this.body.angularVelocity = body.rotation.z > 0 ? new Vector3(0, 0, -0.5f) : body.rotation.z < 0 ? new Vector3(0, 0, 0.5f) : Vector3.zero;
            }
           
        } else
        {
            dropTimer = 0;
            // AR
            float coordZ, coordX, coordY, force;
            body.constraints = RigidbodyConstraints.None;
            force = Sce1Script.TotalForce;
            coordZ = force.Map(0,
                    Sce1Script.TotalForce + Sce2Script.TotalForce,
                    -10,
                    10);
            coordX = body.rotation.x;
            coordY = body.rotation.y;
            //body.MoveRotation(currentRotation.normalized);
            if(coordZ < body.rotation.z-0.5 && coordZ > body.rotation.z+0.5)
                body.MoveRotation(Quaternion.Euler(new Vector3(0, 0, coordZ)));
            //body.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    /*public void onRotationZ()
    {
        if (body.rotation.z > currentRotation.z - 0.1 && body.rotation.z < currentRotation.z + 0.1)
            body.constraints = RigidbodyConstraints.FreezeAll;
    }*/
}

public static class ExtensionMethods
{
    public static float Map(this float valeur, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        //dynamic valeur = value, minFrom = fromSource, maxFrom = toSource, minTo = fromTarget, maxTo = toTarget;
        //return (value - minFrom) / (maxFrom - minFrom) * (maxTo - minTo) + minTo;
        float res = (float)Math.Round((valeur - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget, 1);
        return res;
    }
}