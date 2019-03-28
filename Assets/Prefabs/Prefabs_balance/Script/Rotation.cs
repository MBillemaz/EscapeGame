using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public float max, min;
    public Quaternion originalRotationValue;
    float rotationResetSpeed = 0.5f;
    // Use this for initialization

    void Start()
    {
        originalRotationValue = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FixedUpdate()
    {

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        /*currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
         transform.localRotation = Quaternion.Euler(currentRotation);

         currentRotation = transform.localRotation.eulerAngles;*/
        /*if (currentRotation.x <= min)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
           
        }

        if (currentRotation.x >= max)
        {
            //Freeze all rotations
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            /* StartCoroutine (timer());
        }
        if (currentRotation.x > min)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.None;
        }
        */
        if (currentRotation.x > max || currentRotation.x < min)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, rotationResetSpeed * Time.time);
        }

    }
}
