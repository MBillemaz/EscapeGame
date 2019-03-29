using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socle_1 : MonoBehaviour {

    // Gestion de la masse
    Rigidbody n_rigidbody;
    // Parametre par default sur unity
    public float ForceDefault;
    // Parametre variable de la force
    float ForceVariable;
    // Parametre Total de la force 
   public float TotalForce;

    // Inistialisation du socle 1
    void Start () {
        n_rigidbody = GetComponent<Rigidbody>();
        n_rigidbody.mass = ForceDefault;
        TotalForce = ForceDefault;

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Pilier" || col.gameObject.name == "Pied")
        {
            TotalForce = 0;
        }
        else
        {
            ForceVariable = col.rigidbody.mass;
            TotalForce = ForceVariable + TotalForce;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Pilier" || col.gameObject.name == "Pied")
        {
            TotalForce = 0;
        }
        else
        {
            ForceVariable = col.rigidbody.mass;
            TotalForce = TotalForce - ForceVariable;
        }
    }

}
