using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script pour jouer 1 seul son au moment où on le touche
/// </summary>
public class Sound_OneTime : MonoBehaviour
{
    //1ere source audio
    public AudioSource sound1;
    public bool hasBeenTriggered;

    //Fonction pour jouer la source audio 1
    public void playSound1()
    {
        sound1.Play();
    }

    //Quand le trigger est activé la première fois, le son est joué.
    public void OnTriggerEnter()
    {
        if (!hasBeenTriggered)
        {
            playSound1();
            hasBeenTriggered = true;
        }
    }
    //Pour faire fonctionner le script il faut créer un Object vide et lui ajouter une Audio Source contenant le son à effectuer
}
