using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script pour jouer 1 seul son au moment où on le touche
/// </summary>
public class Sound_OneSound : MonoBehaviour
{
    //1ere source audio
    public AudioSource sound1;

    //Fonction pour jouer la source audio 1
    public void playSound1()
    {
        sound1.Play();
    }

    //Quand le trigger est activé, le son de la source 1 est joué
    public void OnTriggerEnter()
    {
        playSound1();
    }
    //Pour faire fonctionner le script il faut créer un Object vide et lui ajouter une Audio Source contenant le son à effectuer
}
