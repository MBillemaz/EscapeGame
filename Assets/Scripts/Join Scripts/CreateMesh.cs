using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CreateMesh : MonoBehaviour {

    private LineRenderer line;
    private MeshFilter filter;
    private List<Vector3> points = new List<Vector3>();
    public bool AddCollider;
    private MeshCollider meshCollider;
    private void Init()
    {
        line = GetComponent<LineRenderer>();
        filter = GetComponent<MeshFilter>();
        for (var i = 0; i < line.positionCount - 1; i++)
        {
            points.Add(line.GetPosition(i));
        }
    }


    public void DrawMesh()
    {
        Init();
        Vector3[] vertices = new Vector3[points.Count];

        vertices = points.ToArray();


        Mesh mesh = new Mesh();
        line.BakeMesh(mesh);
        mesh.RecalculateNormals();
        filter.sharedMesh = mesh;
    }
}


#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(CreateMesh))]
public class CreateMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var scripts = targets.OfType<CreateMesh>();
        if (GUILayout.Button("Create Mesh"))
            foreach (var script in scripts)
                script.DrawMesh();
    }
}
#endif