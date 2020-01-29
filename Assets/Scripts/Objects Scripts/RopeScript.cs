using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{

    public float RigidbodyMass = 1f;
    public float ColliderRadius = 1f;
    public float JointSpring = 0.1f;
    public float JointDamper = 20f;
    public Vector3 RotationOffset;
    public Vector3 PositionOffset;

    protected List<Transform> CopySource;
    protected List<Transform> CopyDestination;
    protected static GameObject RigidBodyContainer;

    void Awake()
    {
        CopySource = new List<Transform>();
        CopyDestination = new List<Transform>();

        //add children
        AddChildren(transform);
    }

    private void AddChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);

            //rigidbody
            var childRigidbody = child.gameObject.AddComponent<Rigidbody>();
            childRigidbody.useGravity = true;
            childRigidbody.isKinematic = false;
            childRigidbody.freezeRotation = true;
            childRigidbody.mass = RigidbodyMass;

            //collider
            var collider = child.gameObject.AddComponent<MeshCollider>();
            //collider.center = Vector3.zero;
            collider.convex = true;

            //DistanceJoint
            var joint = child.gameObject.AddComponent<DistanceJoin3D>();
            joint.connectedRigidbody = parent;
            joint.determineDistanceOnStart = true;
          //  joint.spring = JointSpring;
            joint.damper = JointDamper;
            //joint.determineDistanceOnStart = false;
            joint.distance = Vector3.Distance(parent.position, child.position);

            //Colision Dection
            var collision = child.gameObject.AddComponent<CollisionScript>();

            //add copy source
            CopySource.Add(child.transform);
            CopyDestination.Add(child);

            AddChildren(child);
        }
    }

    public void Update()
    {
        for (int i = 0; i < CopySource.Count; i++)
        {
            //CopyDestination[i].position = CopySource[i].position + PositionOffset;
            //CopyDestination[i].rotation = CopySource[i].rotation * Quaternion.Euler(RotationOffset);
        }
    }
}
