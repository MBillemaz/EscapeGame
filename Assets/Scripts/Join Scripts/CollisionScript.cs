using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision colision)
    {
        // If Rope collide a arrow, then destroy join
        if (colision.collider.gameObject.GetComponent<Arrow>())
        {
            Destroy(this.GetComponent<DistanceJoin3D>());
        }
    }
}
