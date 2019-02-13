using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<FixedJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
