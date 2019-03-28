using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public float min, max;
    public Quaternion originalRotationValue;
    float rotationResetSpeed = 0.5f;
    private Rigidbody body;
    // Use this for initialization

    void Start()
    {
        originalRotationValue = transform.rotation;
        this.body = GetComponent<Rigidbody>();
    }


    void Update()
    {

        Quaternion currentRotation = transform.rotation;
        float x = currentRotation.x;
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
        Debug.Log(body.angularVelocity.x);
        //Debug.Log("before " + min + " " + max + " "+  x + " " + currentRotation.y + " " + currentRotation.z);
        if (x > max && body.angularVelocity.x > 0)
        {
            // transform.rotation = Quaternion.Euler(max, 0, 0);
            // transform.localRotation.eulerAngles = 3;
            //body.isKinematic = true;
            //body.angularVelocity = -body.angularVelocity*3;
            body.angularVelocity = Vector3.zero;
            // Debug.Log("after max " + transform.rotation.x + " " + transform.rotation.y + " " + transform.rotation.z);
            //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, rotationResetSpeed * Time.time);
        }
        else if (x < min && body.angularVelocity.x < 0)
        {
            // transform.rotation = Quaternion.Euler(min, 0, 0);
            //body.isKinematic = true;
            // Debug.Log("after min " + transform.rotation.x + " " + transform.rotation.y + " " + transform.rotation.z);
            //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, rotationResetSpeed * Time.time);
        } else
        {
            //body.isKinematic = false;
        }

    }
}
