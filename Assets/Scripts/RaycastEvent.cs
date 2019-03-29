using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastEvent : MonoBehaviour {

    protected Text text;
	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
	}

    
	
	public void Trigger()
    {

        Text parentText = transform.parent.GetChild(1).GetComponent<Text>();
        if(text.text == "L'Homme")
        {
            parentText.text = "PERFECT !";
        } else
        {

        }
    }
}
