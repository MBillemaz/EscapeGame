using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	
	// Update is called once per frame
	void Update () {
        Text[] text = GetComponentsInChildren<Text>();

        switch (chiffre)
        {
            case Chiffres.zero:
                text[0].text = 0.ToString();
                break;
            case Chiffres.un:
                text[0].text = 1.ToString();
                break;
            case Chiffres.deux:
                text[0].text = 2.ToString();
                break;
            case Chiffres.trois:
                text[0].text = 3.ToString();
                break;
            case Chiffres.quatre:
                text[0].text = 4.ToString();
                break;
            case Chiffres.cinq:
                text[0].text = 5.ToString();
                break;
            case Chiffres.six:
                text[0].text = 6.ToString();
                break;
            case Chiffres.sept:
                text[0].text = 7.ToString();
                break;
            case Chiffres.huit:
                text[0].text = 8.ToString();
                break;
            case Chiffres.neuf:
                text[0].text = 9.ToString();
                break;
            default:
                break;
        }
    }
}
