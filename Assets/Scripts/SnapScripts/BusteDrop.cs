using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusteDrop: MonoBehaviour, SnapActionInterface {

    private bool hasItem;

    private GameObject dropObject;
    public void ToggleHasItem()
    {
        hasItem = !hasItem;
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

    public void SnapAction(object args)
    {
    }
}
