using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {
    public GameObject ropeModel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<Cutter>() && !Mesh_Cutter.currentlyCutting)
        {
            GameObject newRope = Mesh_Cutter.Cut(this.gameObject, transform.position, transform.right,null, true, false);

            Rigidbody rigibody = CopyComponent<Rigidbody>(this.transform.GetComponent<Rigidbody>(), newRope);

            if(transform.childCount > 0)
            {
                Transform child = this.transform.GetChild(0);

                CopyComponent<Collider>(this.transform.GetComponent<Collider>(), newRope);
                child.GetComponent<CharacterJoint>().connectedBody = rigibody;
                child.parent = newRope.transform;
            }
           
            StopCutting();
            newRope.transform.parent = ropeModel.transform.parent;
            CollisionScript col = newRope.AddComponent<CollisionScript>();
            col.ropeModel = newRope;
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
