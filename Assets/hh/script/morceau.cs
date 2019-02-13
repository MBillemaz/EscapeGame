using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morceau : MonoBehaviour {

    // Use this for initialization
    public GameObject fils;
    public static int n = 0;
	void Start () {
        n++;
        fils.GetComponent<Rigidbody>().AddExplosionForce(2, fils.transform.position, 2, 2);
    }

    // Update is called once per frame
    void Update () {
        //fils.transform.parent.SetParent(GameObject.FindGameObjectWithTag("Bloc").transform);
	}
}
