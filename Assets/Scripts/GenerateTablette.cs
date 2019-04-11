using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTablette : MonoBehaviour {

    // Use this for initialization
    public Transform spawnPoint;
    public GameObject tablette;
    private bool hasSpawn = false;
    public void Spawn()
    {
        if (!hasSpawn)
        {
            GetComponent<AudioSource>().Play();
            hasSpawn = true;
            tablette.transform.position = spawnPoint.position;
            tablette.GetComponent<Rigidbody>().useGravity = true;
        }

    }
}
