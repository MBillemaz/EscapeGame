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
        Debug.Log("Vase Created");
        this.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log(this.GetComponent<Rigidbody>().useGravity);

    }
	
	// Update is called once per frame
	void Update () {
	}

    //Check if there is a collision with our object
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision:" + collision);
        // Instantitate explosion effect
        //Instantiate(explosionEffect, transform.position, Quaternion.identity);


        // Create Fractured Vase after half a second
        StartCoroutine(CreateFracturedVase(0.5f));
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
        Debug.Log("Vase broken");
    }
    private IEnumerator CreateFracturedVase(float waitTime)
    {
        // Waiting for seconds
        yield return new WaitForSeconds(waitTime);

        //Instantiate broken vase
        GameObject fracturedVaseObj = Instantiate(fracturedVase, transform.position, Quaternion.identity);


    }
}

