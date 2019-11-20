using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Menu : MonoBehaviour
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

    public void OnTriggerEnter()
    {
        if (!hasBeenTriggered)
        {
            playmetalDrop1();
            hasBeenTriggered = true;
        }
        else
        {
            playmetalDrop2();
        }
    }
}
