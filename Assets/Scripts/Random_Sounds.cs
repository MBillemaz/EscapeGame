using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script pour jouer 1 son aléatoire
/// </summary>
public class Random_Sounds : MonoBehaviour {

    //Source audio
    public AudioSource sound;

    //Booléen pour détecter si l'objet a déja été touché ou pas
    public bool hasBeenTriggered = false;

    //Tableau d'AudioClip
    public AudioClip[] audioClipsArray;

    //On récupère la source audio
    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    //Quand le trigger est activé, un audio clip aléatoire dans le tableau audioClipsArray est joué
    public void OnTriggerEnter()
    {
        sound.clip = audioClipsArray[Random.Range(0, audioClipsArray.Length)];
        sound.PlayOneShot(sound.clip);
    }

    //Pour faire fonctionner le script, il faut ajouter une Audio Source vide et décocher Play On Awake
}