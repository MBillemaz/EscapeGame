using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    public AudioSource metalDrop1;
    public AudioSource metalDrop2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playmetalDrop1()
    {
        metalDrop1.Play();
    }

    public void playmetalDrop2()
    {
        metalDrop2.Play();
    }

    void onTriggerEnter()
    {
        metalDrop1.Play();
    }
}
