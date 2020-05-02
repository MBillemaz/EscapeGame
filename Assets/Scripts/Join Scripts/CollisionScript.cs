using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {
    public GameObject ropeModel;
    // Use this for initialization
    void Start () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<Cutter>() && !Mesh_Cutter.currentlyCutting)
        {
            StartCoroutine(CutRope());
        }
    }
    IEnumerator CutRope()
    {
        GameObject newRope = Mesh_Cutter.Cut(this.gameObject, transform.position, transform.right, null, true, false);
        if (newRope && newRope.GetComponent<MeshFilter>().sharedMesh.vertexCount > 0
            )
        {
            Destroy(this.transform.GetComponent<Collider>());
            CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
            Rigidbody rigibody = newRope.AddComponent<Rigidbody>();

            GameObject rope = new GameObject("rope");
            rope.transform.position = this.transform.position;
            rope.transform.parent = ropeModel.transform.parent;
            newRope.transform.parent = rope.transform;

            CollisionScript col = newRope.AddComponent<CollisionScript>();
            col.ropeModel = rope;

            if (transform.childCount > 0)
            {
                Transform child = this.transform.GetChild(0);
                newRope.AddComponent<CapsuleCollider>();
                child.GetComponent<CharacterJoint>().connectedBody = rigibody;
                child.parent = newRope.transform;
            }

            AddChildren(rope.transform, rope);
        }
        StopCutting();
        Destroy(newRope, 0.5f);
        yield return new WaitForSeconds(0f);
    }


    void AddChildren(Transform parent, GameObject rope)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            child.GetComponent<CollisionScript>().ropeModel = rope;
            AddChildren(child, rope);
        }
    }
    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
    void StopCutting()
    {
        Mesh_Cutter.currentlyCutting = false;
    }
}
