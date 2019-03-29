using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletteSupport : MonoBehaviour, SnapActionInterface {

    public bool hasItem;
    private Renderer rend;
    GameObject dropObject;
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
        Color lightGrey = new Color(0.75f, 0.75f, 0.75f, 1f);
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", lightGrey);
        this.hasItem = false;
    }

    public void SnapAction(object number)
    {
        isValueCorrect = false;
        if (number.ToString() == value.ToString())
        {
            isValueCorrect = true;
        }
        FindObjectOfType<SupportCode>().UpdateCodeBool();
    }

    public void ToggleHasItem()
    {
        this.hasItem = !this.hasItem;
    }

    public bool HasItem()
    {
        return hasItem;
    }

    public void setDropObject(GameObject obj)
    {
        dropObject = obj;
    }

    public GameObject DropObject()
    {
        return dropObject;
    }
}
