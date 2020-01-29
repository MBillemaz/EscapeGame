using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceJoin3D : MonoBehaviour {

    public Transform connectedRigidbody;
    public bool determineDistanceOnStart = true;
    public float distance;
    public float spring = 0.1f;
    public float damper = 5f;


    void Start()
    {
        if (determineDistanceOnStart && connectedRigidbody != null)
            distance = Vector3.Distance(GetComponent<Rigidbody>().position, connectedRigidbody.position);
    }

    void FixedUpdate()
    {

        var connection = GetComponent<Rigidbody>().position - connectedRigidbody.position;
        var distanceDiscrepancy = distance - connection.magnitude;

        GetComponent<Rigidbody>().position += distanceDiscrepancy * connection.normalized;

        var velocityTarget = connection + (GetComponent<Rigidbody>().velocity + Physics.gravity * spring);
        var projectOnConnection = Vector3.Project(velocityTarget, connection);
        GetComponent<Rigidbody>().velocity = (velocityTarget - projectOnConnection) / (1 + damper * Time.fixedDeltaTime);


    }
}
