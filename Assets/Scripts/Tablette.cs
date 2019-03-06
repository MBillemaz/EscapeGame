using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablette : MonoBehaviour {

    public enum Chiffres // your custom enumeration
    {
        zero,
        un,
        deux,
        trois,
        quatre,
        cinq,
        six,
        sept,
        huit,
        neuf
    };    
    public Chiffres chiffre; // this public var should appear as a drop down
    private Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();        
    }
	
	// Update is called once per frame
	void Update () {
        switch (chiffre)
        {
            case Chiffres.zero:
                rend.material.SetColor("_Color", Color.black);
                break;
            case Chiffres.un:
                rend.material.SetColor("_Color", Color.blue);
                break;
            case Chiffres.deux:
                rend.material.SetColor("_Color", Color.clear);
                break;
            case Chiffres.trois:
                rend.material.SetColor("_Color", Color.cyan);
                break;
            case Chiffres.quatre:
                rend.material.SetColor("_Color", Color.gray);
                break;
            case Chiffres.cinq:
                rend.material.SetColor("_Color", Color.green);
                break;
            case Chiffres.six:
                rend.material.SetColor("_Color", Color.magenta);
                break;
            case Chiffres.sept:
                rend.material.SetColor("_Color", Color.red);
                break;
            case Chiffres.huit:
                rend.material.SetColor("_Color", Color.white);
                break;
            case Chiffres.neuf:
                rend.material.SetColor("_Color", Color.yellow);
                break;
            default:
                break;
        }
    }
}
