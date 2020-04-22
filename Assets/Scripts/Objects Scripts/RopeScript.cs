using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    private enum ColliderType { 
     MeshCollider,
     SphereCollider
    }

    private enum Join
    {
        DistanceJoin,
        HingeJoin
    }


    public float RigidbodyMass = 1f;
    public float ColliderRadius = 0.1f;
    public float JointSpring = 0.1f;
    public float JointDamper = 20f;
    public Vector3 RotationOffset;
    public Vector3 PositionOffset;
    public GameObject ropeModel;
    [SerializeField] Join joinType;
    [SerializeField] ColliderType colliderType;
    public bool Gravity = true;
    protected List<Transform> CopySource;
    protected List<Transform> CopyDestination;
    protected static GameObject RigidBodyContainer;

    void Start()
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
            childRigidbody.useGravity = Gravity;
            childRigidbody.isKinematic = false;
            childRigidbody.freezeRotation = true;
            childRigidbody.mass = RigidbodyMass;

            //add sphere collider or mesh collider
            if(colliderType == ColliderType.SphereCollider)
            {
                var collider = child.gameObject.AddComponent<SphereCollider>();
                collider.center = Vector3.zero;
                collider.radius = ColliderRadius;
            }
            else
            {
                var collider = child.gameObject.AddComponent<MeshCollider>();
                collider.convex = true;
            }


            //DistanceJoint
            if(joinType == Join.DistanceJoin)
            {
                var joint = child.gameObject.AddComponent<DistanceJoin3D>();
                joint.connectedRigidbody = parent;
                joint.determineDistanceOnStart = true;
                joint.spring = JointSpring;
                joint.damper = JointDamper;
                joint.determineDistanceOnStart = false;
                joint.distance = Vector3.Distance(child.position, parent.position);
            } else
            {
                var joint = child.gameObject.AddComponent<CharacterJoint>();
                joint.connectedBody = parent.GetComponent<Rigidbody>();
                SoftJointLimitSpring softJointLimit = joint.twistLimitSpring;
                softJointLimit.damper = JointDamper;
                softJointLimit.spring = JointSpring;
                childRigidbody.freezeRotation = false;

            }        


            //Colision Dection
            var collision = child.gameObject.AddComponent<CollisionScript>();
            collision.ropeModel = ropeModel;

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
            CopyDestination[i].position = CopySource[i].position + PositionOffset;
            CopyDestination[i].rotation = CopySource[i].rotation * Quaternion.Euler(RotationOffset);
        }
    }
}
