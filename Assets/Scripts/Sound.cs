using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public AudioSource metalDrop1;
    public AudioSource metalDrop2;
    public bool hasBeenTriggered = false;

    public void playmetalDrop1()
    {
        metalDrop1.Play();
    }

    public void playmetalDrop2()
    {
        metalDrop2.Play();
    }

    void OnTriggerEnter()
    {
        if (hasBeenTriggered)
        {
            metalDrop2.Play();
        }
        else
        {
            metalDrop1.Play();
            hasBeenTriggered = true;
        }

    }
}
