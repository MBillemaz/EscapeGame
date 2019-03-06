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

    
	
	public void Test()
    {

        Text parentText = transform.parent.GetChild(1).GetComponent<Text>();
        Debug.Log(transform.parent);
        Debug.Log(transform.parent.GetChild(0).name);
        Debug.Log(text.text);
        if(text.text == "Reponse 1")
        {
            parentText.text = "PERFECT !";
        } else
        {
            parentText.text = "NEIN NEIN NEIN !";
        }
    }
}
