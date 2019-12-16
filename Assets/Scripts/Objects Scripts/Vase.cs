using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

//Vase instance to break
public class Vase : MonoBehaviour {

    // Use this for initialization
    public GameObject fracturedVase;
    public GameObject explosionEffect;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    //Check if there is a collision with our object
    void OnCollisionEnter(Collision collision)
    {
        // Instantiate broken object
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GameObject fracturedVaseObj = Instantiate(fracturedVase, transform.position, Quaternion.identity);
        Rigidbody[] rigidbodies = fracturedVase.GetComponentsInChildren<Rigidbody>();
        //Add explosion effect
        if (rigidbodies.Length > 0)
            {
                foreach(Rigidbody body in rigidbodies )
                {
                    body.AddExplosionForce(500, transform.position, 1);
                }
            }
        // Destroy not broken object
        Destroy(this.gameObject);
    }
}

