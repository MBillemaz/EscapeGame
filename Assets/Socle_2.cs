using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socle_2 : MonoBehaviour {

    // Gestion de la masse
    Rigidbody n_rigidbody;
    // Parametre par default sur unity
    public float ForceDefault;
    // Parametre variable de la force
    float ForceVariable;
    // Parametre Total de la force 
    public float TotalForce = 0;

    // Inistialisation du socle 2
    void Start()
    {
        n_rigidbody = GetComponent<Rigidbody>();
        n_rigidbody.mass = ForceDefault;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Pilier" || col.gameObject.name == "Pied")
        {
            TotalForce = 0;
        }
        else
        {
            ForceVariable = col.rigidbody.mass;
            TotalForce = ForceVariable + ForceDefault;
        }
    }

}
