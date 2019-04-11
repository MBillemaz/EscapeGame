using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapKeyAction : MonoBehaviour, SnapActionInterface
{
    private bool hasItem = false;

    private bool isUsed = false;

    private bool finished = false;

    private GameObject dropObject;

    public Rigidbody coffreRigid;

    public AudioClip chestOpeningAudioClip;

    public void SnapAction(object name)
    {
        if (!isUsed)
        {
            isUsed = true;
            //Start key sound
            GetComponent<AudioSource>().Play();
        }
        
    }

    void Update()
    {
        if(isUsed && !finished)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = chestOpeningAudioClip;
            coffreRigid.angularVelocity = new Vector3(0, 0, 0.5f);
            GetComponent<AudioSource>().Play();
        }
        if (coffreRigid.rotation.x <= -0.75)
        {
            coffreRigid.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<AudioSource>().Stop();
            finished = true;
        }
    }

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
}
