using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonpoids : MonoBehaviour
{
    Rigidbody n_rigidbody;
    float positiony;
    float positionini;
    public float force = 40f;
    /*
    public Rigidbody _rb;
    public float force;*/
     // Use this for initialization
     void Start()
     {
         n_rigidbody = GetComponent<Rigidbody>();
         positionini = gameObject.transform.position.y;

     }

     // Update is called once per frame
     void Update()
     {
        /*if (gameObject.transform.position.y == positionini)
            n_rigidbody.AddForce(0f,0f,0f);*/
            if (gameObject.transform.position.y <= positionini)
            n_rigidbody.AddForce(0, force * Time.deltaTime, 0, ForceMode.VelocityChange);
        /*n_rigidbody.AddForce(0, force*Time.deltaTime,0,ForceMode.Impulse);*/
    }
     
    /*void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        _rb.AddForce(0, force * Time.deltaTime, 0,ForceMode.VelocityChange);
        
    }*/
}