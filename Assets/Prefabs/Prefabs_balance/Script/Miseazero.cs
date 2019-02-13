using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miseazero : MonoBehaviour
{

    public Quaternion originalRotationValue; // declare this as a Quaternion
    float rotationResetSpeed = 1.0f;

    /*public Rigidbody r_Rigidbody;*/
    public float max = 133;
    public float min = 47;

    // Use this for initialization
    void Start()
    {
        originalRotationValue = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        /* Vector3 currentRotation = transform.localRotation.eulerAngles;
         if (currentRotation.x >= max)
            {
                r_Rigidbody.rotation = Quaternion.Euler(90f,0f,0f);
            }

        }*/
        //rotate selected piece
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        if (currentRotation.x > max || currentRotation.x<min)
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed);
    }
}