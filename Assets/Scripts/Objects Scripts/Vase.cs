using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

//Vase instance to break
public class Vase : MonoBehaviour {

    // Use this for initialization
    public GameObject fracturedVase;
    public GameObject explosionEffect;
    private bool hasCollide = false;
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	}

    //Check if there is a collision with our object
    void OnCollisionEnter(Collision collision)
    {
        if( !hasCollide)
        {
            Debug.Log("Collision:" + collision.collider.gameObject.name);
            // Instantitate explosion effect
            //Instantiate(explosionEffect, transform.position, Quaternion.identity);


            // Create Fractured Vase after half a second
            //Instantiate broken vase
            StartCoroutine(CreateFracturedVase(0.0f));

            Rigidbody[] rigidbodies = fracturedVase.GetComponentsInChildren<Rigidbody>();

            //Add explosion effect
            if (rigidbodies.Length > 0)
            {
                foreach (Rigidbody body in rigidbodies)
                {
                 // body.AddExplosionForce(1, transform.position, 1);
                }
            }
            // Destroy not broken object
            Destroy(this.gameObject);
            hasCollide = true;
        }

    }
    private IEnumerator CreateFracturedVase(float waitTime)
    {

        //Instantiate broken vase
        GameObject fracturedVaseObj = Instantiate(fracturedVase, transform.position, Quaternion.identity);
        for (int i = 0; i < fracturedVaseObj.transform.childCount; i++)
        {
            Transform child = fracturedVaseObj.transform.GetChild(i);
            child.transform.localScale = new Vector3(20, 20, 20);

            // Each broken part can be taken by player
            child.gameObject.AddComponent<Interactable>();

            // Add Rigibody
            Rigidbody rigibody = child.gameObject.AddComponent<Rigidbody>();
            rigibody.useGravity = true;
            rigibody.isKinematic = false;
            rigibody.freezeRotation = true;
            rigibody.mass = 1f;


            // Add Mesh collider
            var collider = child.gameObject.AddComponent<MeshCollider>();
            collider.convex = true;
        }

        // Waiting for seconds
        yield return new WaitForSeconds(waitTime);

    }
}

