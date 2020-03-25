using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {
    private string cuttableTag = "Cuttable";
    public GameObject ropeModel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<Cutter>())
        {
            GameObject newRope = Mesh_Cutter.Cut(ropeModel, transform.position, transform.right,null, true, false);
            StopCutting();
            this.transform.SetParent(newRope.transform);

            ConstructRope(ropeModel);
           /// RemoveInitialConfiguration(newRope.transform, newRope);

            newRope.GetComponent<SkinnedMeshRenderer>().rootBone = this.transform;

        }
    }

    void ConstructRope(GameObject ropeModel)
    {
        List<Transform> bones = new List<Transform>();
        AddBone(ropeModel.transform, bones);
        ropeModel.GetComponent<SkinnedMeshRenderer>().bones = bones.ToArray();

        List<BoneWeight> boneWeights =new List<BoneWeight>();
        SkinnedMeshRenderer renderer = Ohohoh.UpdateCollisionMesh(ropeModel);
        Debug.Log(renderer.bones.Length);

    }

    void BindSkin(GameObject ropeModel)
    {
        SkinnedMeshRenderer renderer = ropeModel.GetComponent<SkinnedMeshRenderer>();


        /*MESH*/
        Mesh sourceMesh = renderer.sharedMesh;
        sourceMesh.RecalculateNormals();

        BoneWeight[] weights = new BoneWeight[sourceMesh.vertexCount];
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i].boneIndex0 = 0;
            weights[i].weight0 = 1;
        }
        sourceMesh.boneWeights = weights;



        /*SKELETON*/
        Transform[] bones;
        bones = ropeModel.GetComponentsInChildren<Transform>();



        /*BIND POSES*/
        Matrix4x4[] bindPoses = new Matrix4x4[bones.Length];
        for (int i = 0; i < bindPoses.Length; i++)
        {
            bindPoses[i] = bones[i].worldToLocalMatrix * transform.localToWorldMatrix;
        }


        /*END*/
        sourceMesh.bindposes = bindPoses;
        renderer.bones = bones;
        renderer.sharedMesh = sourceMesh;
    }

    void AddBone(Transform model, List<Transform> bones)
    {
        for (int i = 0; i < model.childCount; i++)
        {
            var child = model.GetChild(i);
            bones.Add(child);
            AddBone(child, bones);
        }
    }

    void RemoveInitialConfiguration(Transform parent, GameObject initialObject)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);

            ////remove rigidbody
            Destroy(child.gameObject.GetComponent<Rigidbody>());

            //remove sphere collider

            Destroy(child.gameObject.GetComponent<SphereCollider>());

            //DistanceJoint
            Destroy(child.gameObject.GetComponent<DistanceJoin3D>());


            RemoveInitialConfiguration(child, initialObject);
        }
        Debug.Log(parent.gameObject.name);
        if (parent.childCount == 0)
        {
            Debug.Log("Add Rope");
            AddRope(initialObject);
        }
    }

    void AddRope(GameObject newRope)
    {
        RopeScript rope = newRope.AddComponent<RopeScript>();
        rope.ropeModel = newRope;
    }
         
   
    void StopCutting()
    {
        Mesh_Cutter.currentlyCutting = false;
    }
}
