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

        GenerateTablette script = transform.parent.GetComponent<GenerateTablette>();
        ColorBlock colors = GetComponent<Button>().colors;
        if (text.text == "L'Homme")
        {
            colors.normalColor = Color.green;
            GetComponent<Button>().colors = colors;
            script.Spawn();
        } else
        {

            colors.normalColor = Color.red;
            GetComponent<Button>().colors = colors;

        }
    }
}
