using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RopeScript : MonoBehaviour
{
    private enum ColliderType { 
     MeshCollider,
     SphereCollider,
     CapsuleCollider
    }

    private enum Join
    {
        DistanceJoin,
        HingeJoin
    }


    [SerializeField] float RigidbodyMass = 1f;
    [SerializeField] float ColliderRadius = 0.1f;
    [SerializeField] float JointSpring = 0.1f;
    [SerializeField] float JointDamper = 20f;
    [SerializeField] Vector3 RotationOffset;
    [SerializeField] Vector3 PositionOffset;
    [SerializeField] Join joinType;
    [SerializeField] ColliderType colliderType;
    [SerializeField] bool Gravity = true;
    private List<Transform> CopySource;
    private List<Transform> CopyDestination;

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


            //add sphere collider or mesh collider
            if(colliderType == ColliderType.SphereCollider)
            {
                var collider = child.gameObject.AddComponent<SphereCollider>();
                collider.center = Vector3.zero;
                collider.radius = ColliderRadius;
            }
            //add sphere collider or mesh collider
            if (colliderType == ColliderType.CapsuleCollider)
            {
                var collider = child.gameObject.AddComponent<CapsuleCollider>();
                collider.center = Vector3.zero;
            }
            else
            {
                var collider = child.gameObject.AddComponent<MeshCollider>();
                collider.convex = true;
            }



            //rigidbody
            var childRigidbody = child.gameObject.AddComponent<Rigidbody>();
            childRigidbody.useGravity = Gravity;
            childRigidbody.isKinematic = false;
            //childRigidbody.freezeRotation = true;
            childRigidbody.mass = RigidbodyMass;

            //DistanceJoint
            if (joinType == Join.DistanceJoin)
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
                SoftJointLimitSpring softJointLimit = joint.swingLimitSpring;
                softJointLimit.damper = JointDamper;
                softJointLimit.spring = JointSpring;
                joint.swingLimitSpring = softJointLimit;
            }        


            //Colision Dection
            var collision = child.gameObject.AddComponent<CollisionScript>();
            collision.ropeModel = this.gameObject;
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
