using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction : MonoBehaviour {
    public GameObject brokenObject;

    void OnTriggerEnter(Collider other)
    {
        Instantiate (brokenObject,transform.position,transform.rotation);
        Destroy(gameObject);
    }
    // Use this for initialization
    
}
