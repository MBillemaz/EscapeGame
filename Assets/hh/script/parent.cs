using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent : MonoBehaviour {
    public GameObject parentObjet ;
    public GameObject[] morceauxObjet;
    public float distance;
    public float x = 0;
    public float y = 0;
    public float z = 0;
    public int n = 0;

    // Use this for initialization
    void Start () {
        //Instantiate(parentObjet, transform.position, transform.rotation);
         morceauxObjet = new GameObject[GameObject.FindGameObjectsWithTag("Morceaux").Length];

        /* for (int i = 1; i < parentObjet.Length; i++)
         {
             Debug.Log("Morceaux numero " + i + ": " + parentObjet[i].name);
         }*/
         parentObjet.transform.position.Set(4,1,0);
        x = parentObjet.transform.position.x;
        y = parentObjet.transform.position.y;
        z = parentObjet.transform.position.z;

    }
    void objetProximite()
    {
        GameObject other = GameObject.FindGameObjectWithTag("Morceaux");
        float dist = Vector3.Distance(other.transform.position, parentObjet.transform.position);
       /* print("Nom : " + other.name + " x : " + other.transform.position.x + " y : "
                + other.transform.position.y + " Distance to other: " + dist);
        if (dist < 4)
        {
            print("Objet proche : " + other.name + " x : " + other.transform.position.x + " y : "
                  + other.transform.position.y + " Distance to other: " + dist);
        }*/
        n++;
        //Debug.Log("Iteration : " + n);
    }
    // Update is called once per frame
    void Update () {
        objetProximite();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
