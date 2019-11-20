using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Level1 : MonoBehaviour
{

    public AudioSource vaseDrop1;
    public AudioSource vaseDrop2;

    public bool hasBeenTriggered = false;

    public void playvaseDrop1()
    {
        vaseDrop1.Play();
    }

    public void playvaseDrop2()
    {
        vaseDrop2.Play();
    }

    public void OnTriggerEnter()
    {
        if (!hasBeenTriggered)
        {
            playvaseDrop1();
            hasBeenTriggered = true;
        }
        else
        {
            playvaseDrop2();
        }
    }
}
