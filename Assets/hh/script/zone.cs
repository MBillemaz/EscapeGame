﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class zone : MonoBehaviour {

    public GameObject[] tmpObjets;
    public GameObject [] busteAutel;
    public GameObject[] listMorceaux;
    public int i = 0;

    
    void Start()
    {
        //Instantier l'objet entier a reconstituer
        listMorceaux = new GameObject[GameObject.FindGameObjectsWithTag("Morceaux").Length];
        listMorceaux = GameObject.FindGameObjectsWithTag("Morceaux");

        tmpObjets = new GameObject[GameObject.FindGameObjectsWithTag("Morceaux").Length];

        busteAutel = new GameObject[GameObject.FindGameObjectsWithTag("buste").Length];
        busteAutel = GameObject.FindGameObjectsWithTag("autel");
    }
    private void allMorceaux()
    {
        if (this.i == this.tmpObjets.Length)
        {
            Debug.Log("On va détruire l'objet");
            //Appel de l'animation quand tous les objets sont présents

            //Destruction de tous les morceaux
            for (var y = 0; y < i; y++)
            {
                Destroy(tmpObjets[y]);
            }

        }
    }
    // Détecte si un morceaux a été trouvé, si tous les morceaux sont trouvés fait appel a l'animation pour reconstituer l'objet
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Nom : " + other.name);

        GameObject tmp = null;
        bool a = false;
        var tmp2 = 0;
        if (other.CompareTag("Morceaux"))
        {

        
        for (int z = 0; z < listMorceaux.Length; z++)
        {
            if (listMorceaux[z].name.Equals(other.name))
            {
               // Debug.Log(listMorceaux[z].transform.position);
                tmp = listMorceaux[z];
  
                    for (int y = 0; y < tmpObjets.Length; y++)
                    {

                        if (tmpObjets[y] != null)
                        {
                            if (tmpObjets[y].Equals(tmp))
                            {
                                tmp2 = 1;
                                
                            }
                        }

                    }
                    // si l'objet n'est pas présent on l'ajoute et on incremente i
                    if (tmp2 == 0)
                    {
                        tmpObjets[i] = tmp;                 
                        i++;
                        for (int x = 0; x < busteAutel.Length; x++)
                        {
                            busteAutel[x].name.Equals(other.name);
                            Renderer rend = busteAutel[x].GetComponent<Renderer>();
                            rend.enabled = true;
                            Renderer rend1 = other.GetComponent<Renderer>();
                            rend1.enabled = false;
                        }
                        Debug.Log(i);
                        Debug.Log(tmp);
                    }
                }
            allMorceaux();

        }
        }
    }
        
        //morceaux.get
        //GameObject tmp = GameObject.FindGameObjectWithTag(other.name);
        // On regarde si l'objet n'est pas deja dans la liste des morceaux formant l'objet principale
        
    }

   
    

