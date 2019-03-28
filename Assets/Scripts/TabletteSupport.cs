using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletteSupport : MonoBehaviour {

    private Renderer rend;
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
    public Chiffres value; // this public var should appear as a drop down
    public bool isValueCorrect = false;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.grey);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tablette")
        {
            isValueCorrect = false;
            if (other.GetComponent<Tablette>().chiffre.ToString() == value.ToString())
            {
                isValueCorrect = true;
            }
            FindObjectOfType<SupportCode>().UpdateCodeBool();
        }
    }
}
