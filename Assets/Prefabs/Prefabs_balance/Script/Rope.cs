﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<HingeJoint>().connectedBody = transform.parent.GetComponent<Rigidbody> ();
	}

}
