using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class frontOfCamera : MonoBehaviour {

    // La position de l'utilisateur est null lors du start. On stocke donc la valeur et dés on lance le traitement dés qu'elle n'est pas null
    Transform userPos = null;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (this.userPos == null)
        {
            this.userPos = VRTK_DeviceFinder.HeadsetTransform();
            this.transform.position = this.userPos.position + new Vector3(0, 0, 10);
        }
    }
}
