using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script pour jouer 2 sons différents au moment du touché avec un autre élément
/// </summary>
public class Sound_TwoSounds : MonoBehaviour
{
    //1ere source audio
    public AudioSource sound1;
    //2eme source audio
    public AudioSource sound2;
    //Booléen pour détecter si l'objet a déja été touché ou pas
    public bool hasBeenTriggered = false;

    //Fonction pour jouer la source audio 1
    public void playSound1()
    {
        sound1.Play();
    }

    //Fonction pour jouer la source audio 2
    public void playSound2()
    {
        sound2.Play();
    }

    //Quand le trigger est activé, si l'objet n'a jamais été touché, le son 1 est activé, sinon c'est le son 2 qui est joué.
    public void OnTriggerEnter()
    {
        if (!hasBeenTriggered)
        {
            playSound1();
            hasBeenTriggered = true;
        }
        else
        {
            playSound2();
        }
    }
}
