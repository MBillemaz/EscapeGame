using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    private Rigidbody body;
    private Quaternion currentRotation;
    public Transform originalPosition;
    private Vector3 originalPos;


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

        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        // Comparaison de la force par défault des socles 
        // Freeze De la rotation de la barre
        if (Sce1Script.ForceDefault == Sce2Script.ForceDefault)
        {
            this.body.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Update()
    {

        // Récuperation et detection des socles 
        GameObject Sce1 = GameObject.Find("Socle 1");
        Socle_1 Sce1Script = Sce1.GetComponent<Socle_1>();

        GameObject Sce2 = GameObject.Find("Socle 2");
        Socle_2 Sce2Script = Sce2.GetComponent<Socle_2>();

         
        if (Sce1Script.TotalForce != 0)
        {
            this.body.constraints = RigidbodyConstraints.None;

        }

        if (Sce2Script.TotalForce != 0)
        {
            this.body.constraints = RigidbodyConstraints.None;

        }


        if (Sce1Script.TotalForce == Sce2Script.TotalForce)
        {
            gameObject.transform.position = originalPos;

        }
    }
}
