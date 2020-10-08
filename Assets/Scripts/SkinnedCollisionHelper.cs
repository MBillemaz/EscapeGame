using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedCollisionHelper : MonoBehaviour {

    public Transform RootBone;
    private Dictionary<Transform, Vector3> bonesPositions;
    SkinnedMeshRenderer meshRenderer;
    MeshCollider collider;
    int bones = 0;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        bonesPositions = new Dictionary<Transform, Vector3>();
        bones = RootBone.childCount;
        AddChildToBones(RootBone);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCollider();
       //GetBoneChildPosition(RootBone);
    }
    void AddChildToBones(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform bone = parent.GetChild(i);
            bonesPositions.Add(bone, bone.position);
            AddChildToBones(bone);
        }
    }

    Vector3 GetRoundPosition(Vector3 vector)
    {
        return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
    }

    private void CreateBlendMesh(SkinnedMeshRenderer skinnedMeshRenderer, Mesh skinnedMesh, string name, bool convex)
    {
        // Detecting how many BlendShapes we have.
        int blendShapeCount = 0;
        blendShapeCount = skinnedMesh.blendShapeCount;
        Debug.Log("BlendShape count bottom: " + blendShapeCount);

        // Applying BlendShapes.
        //if (blendShapeCount != 0)
        //    skinnedMeshRenderer.SetBlendShapeWeight(0, size * 100);

        // Creates a snapshot of the SkinnedMeshRenderer and stores it in the mesh.
        // That skinned mesh renderer should have the shape with the BlendShapes applyied.
        Mesh bakedMesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(bakedMesh);

        // Recalcultate the bounding volume of the mesh from the vertices.
        bakedMesh.RecalculateBounds();
        Debug.Log("Baked mesh bounds: " + bakedMesh.bounds.ToString());

        // Selecting part and destroying MeshCollider in case there is one.
        GameObject child = transform.Find(name).gameObject;
        DestroyImmediate(child.GetComponent<MeshCollider>());

        // Adding MeshCollider and assigning the bakedMesh.
        MeshCollider meshCollider = child.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = bakedMesh;
        meshCollider.convex = convex;
    }

    public void UpdateCollider()
    {

        // Change object collider
     
        Mesh bakedMesh = new Mesh();
        //bakedMesh = collider.sharedMesh;
        meshRenderer.BakeMesh(bakedMesh);
        bakedMesh.RecalculateBounds();
        collider.sharedMesh = null;
        collider.sharedMesh = bakedMesh;
    }
}



