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
    private bool rotate;


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
        }
    }

    private void FixedUpdate() // AR
    {
        GameObject Sce1 = GameObject.Find("Socle 1");
        Socle_1 Sce1Script = Sce1.GetComponent<Socle_1>();

        GameObject Sce2 = GameObject.Find("Socle 2");
        Socle_2 Sce2Script = Sce2.GetComponent<Socle_2>();

        float coordZ, force;
        
        force = Sce1Script.TotalForce - 10;
        coordZ = force.Map(0,
                (Sce1Script.TotalForce - 10) + (Sce2Script.TotalForce - 10),
                -20,
                20);

        if (Math.Abs(Math.Abs(body.transform.rotation.eulerAngles.z.Euler()) - Math.Abs(coordZ)) < 0.5f)
            rotate = false;
        else if (Math.Abs(body.transform.rotation.eulerAngles.z.Euler()) > 20)
            rotate = false;
        else
            rotate = true;

        if(rotate) body.transform.Rotate((coordZ > 0 ? Vector3.forward : Vector3.back) * Time.deltaTime * 10, Space.World);
    }
}

public static class ExtensionMethods
{
    public static float Map(this float valeur, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        float res = (float)Math.Round((valeur - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget, 1);
        return res;
    }

    public static float Euler(this float valeur)
    {
        if (valeur <= 180)
        {
            return valeur;
        }
        else
        {
            valeur -= 180;
            valeur *= -1;
            return (float)Math.Round(-180 - valeur, 1);
        }
    }
}