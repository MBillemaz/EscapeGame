using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_Cutter
{

    public static bool currentlyCutting;
    public static Mesh originalMesh;
    public static GameObject Cut(GameObject _originalGameObject, Vector3 _contactPoint, Vector3 _direction, Material _cutMaterial = null, bool fill = true, bool _addRigigibody = false)
    {
        if (currentlyCutting)
        {
            return null;
        }
        currentlyCutting = true;

        // set the cutter relative to victim

        Plane plane = new Plane(_originalGameObject.transform.InverseTransformDirection(-_direction),
            _originalGameObject.transform.InverseTransformPoint(_contactPoint));

        //get the victims mesh
        originalMesh = _originalGameObject.GetComponent<MeshFilter>() ? _originalGameObject.GetComponent<MeshFilter>().mesh : _originalGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;

        //List for added vertices
        List<Vector3> addedVertices = new List<Vector3>();

        //Create two new meshes
        GeneratedMesh leftMesh = new GeneratedMesh();
        GeneratedMesh rightMesh = new GeneratedMesh();

        int[] submeshIndices;
        int triangleIndexA, triangleIndexB, triangleIndexC;

        for (int i = 0; i < originalMesh.subMeshCount; i++)
        {
            //Fetches the triangle list for the specified sub-mesh on the victim
            submeshIndices = originalMesh.GetTriangles(i);

            for (int j = 0; j < submeshIndices.Length; j += 3)
            {
                triangleIndexA = submeshIndices[j];
                triangleIndexB = submeshIndices[j + 1];
                triangleIndexC = submeshIndices[j + 2];

                MeshTriangle currentTriangle = GetTriangle(triangleIndexA, triangleIndexB, triangleIndexC, i);

                // Get vertices sides
                bool triangleALeftSide = plane.GetSide(originalMesh.vertices[triangleIndexA]);
                bool triangleBLeftSide = plane.GetSide(originalMesh.vertices[triangleIndexB]);
                bool triangleCLeftSide = plane.GetSide(originalMesh.vertices[triangleIndexC]);

                if (triangleALeftSide && triangleBLeftSide && triangleCLeftSide)// left side
                {
                    leftMesh.AddTriangle(currentTriangle);  
                }
                else if (!triangleALeftSide && !triangleBLeftSide && !triangleCLeftSide)// right side
                {
                    rightMesh.AddTriangle(currentTriangle);
                }
                else // Cut the triangle
                {
                    CutTriangle(plane, currentTriangle, triangleALeftSide, triangleBLeftSide, triangleCLeftSide, leftMesh, rightMesh, addedVertices);
                }

            }
        }

        // Get Materials to apply to the new objects
        Material[] mats ;

        if (_originalGameObject.GetComponent<MeshRenderer>())
        {
            mats = _originalGameObject.GetComponent<MeshRenderer>().sharedMaterials;
        }
        else
        {
           mats = _originalGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
        }

        // if there is a material to fill the cut add it to materials list
        if (_cutMaterial)
        {
            if (mats[mats.Length - 1].name != _cutMaterial.name)
            {
                Material[] newmats = new Material[mats.Length + 1];
                mats.CopyTo(newmats, 0);
                newmats[mats.Length] = _cutMaterial;
                mats = newmats;
            }
        }
        int submeshCount = mats.Length - 1;

        if (fill)
        {
            // fill the opennings
            FillCut(addedVertices, plane, leftMesh, rightMesh, submeshCount);
        }

        //// Left Mesh
        Mesh left_HalfMesh = leftMesh.GetMesh();
        left_HalfMesh.name = "Split Mesh Left";

        //// Right Mesh
        Mesh right_HalfMesh = rightMesh.GetMesh();
        right_HalfMesh.name = "Split Mesh Right";

        //// assign the game objects

        if (_originalGameObject.GetComponent<MeshFilter>())
        {
            _originalGameObject.GetComponent<MeshFilter>().mesh = left_HalfMesh;
        }
        else
        {

            _originalGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = left_HalfMesh;
        }


        GameObject leftSideObj = _originalGameObject;
        GameObject rightSideObj = null;
        if (_originalGameObject.GetComponent<MeshRenderer>())
        {
            rightSideObj = new GameObject("right side", typeof(MeshFilter), typeof(MeshRenderer));
            rightSideObj.transform.position = _originalGameObject.transform.position;
            rightSideObj.transform.rotation = _originalGameObject.transform.rotation;
            rightSideObj.GetComponent<MeshFilter>().mesh = right_HalfMesh;
        }
        else if (_originalGameObject.GetComponent<SkinnedMeshRenderer>())
        {
            rightSideObj = new GameObject("right side");
            SkinnedMeshRenderer skinMesh = rightSideObj.AddComponent<SkinnedMeshRenderer>();
            rightSideObj.transform.position = _originalGameObject.transform.position;
            rightSideObj.transform.rotation = _originalGameObject.transform.rotation;
            skinMesh.sharedMesh = right_HalfMesh;
        }


        if (_originalGameObject.transform.parent != null)
        {
            rightSideObj.transform.parent = _originalGameObject.transform.parent;
        }

        rightSideObj.transform.localScale = _originalGameObject.transform.localScale;

        //Add rigibody to the new object
        if (_addRigigibody)
        {
            var rigibody = rightSideObj.AddComponent<Rigidbody>();
            rigibody = _originalGameObject.GetComponent<Rigidbody>();
        }

        // assign materials

        if (_originalGameObject.GetComponent<MeshRenderer>())
        {

            leftSideObj.GetComponent<MeshRenderer>().materials = mats;
            rightSideObj.GetComponent<MeshRenderer>().materials = mats;
        }
        else if (_originalGameObject.GetComponent<SkinnedMeshRenderer>())
        {
            leftSideObj.GetComponent<SkinnedMeshRenderer>().materials = mats;
            rightSideObj.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }

        return rightSideObj;


    }


    private static MeshTriangle GetTriangle(int triangleIndexA, int triangleIndexB, int triangleIndexC, int submeshIndex)
    {
        MeshTriangle triangle = new MeshTriangle(new Vector3[3], new Vector3[3], new Vector2[3], submeshIndex);
        //Vertices
        triangle.Vertices[0] = originalMesh.vertices[triangleIndexA];
        triangle.Vertices[1] = originalMesh.vertices[triangleIndexB];
        triangle.Vertices[2] = originalMesh.vertices[triangleIndexC];

        //Normals
        triangle.Normals[0] = originalMesh.normals[triangleIndexA];
        triangle.Normals[1] = originalMesh.normals[triangleIndexB];
        triangle.Normals[2] = originalMesh.normals[triangleIndexC];

        //UVS
        triangle.Uvs[0] = originalMesh.uv[triangleIndexA];
        triangle.Uvs[1] = originalMesh.uv[triangleIndexB];
        triangle.Uvs[2] = originalMesh.uv[triangleIndexC];

        return triangle;
    }

    private static void CutTriangle(Plane _plane, MeshTriangle _triangle, bool _triangleALeftSide, bool _triangleBLeftSide, bool _triangleCLeftSide,
        GeneratedMesh _leftSide, GeneratedMesh _rightSide, List<Vector3> _addedVertices)
    {
        List<bool> leftSide = new List<bool>();
        leftSide.Add(_triangleALeftSide);
        leftSide.Add(_triangleBLeftSide);
        leftSide.Add(_triangleCLeftSide);


        MeshTriangle leftMeshTriangle = new MeshTriangle(new Vector3[2], new Vector3[2], new Vector2[2], _triangle.SubmeshIndex);
        MeshTriangle rightMeshTriangle = new MeshTriangle(new Vector3[2], new Vector3[2], new Vector2[2], _triangle.SubmeshIndex);
        MeshTriangle meshTriangle = new MeshTriangle(new Vector3[2], new Vector3[2], new Vector2[2], _triangle.SubmeshIndex);

        bool left = false;
        bool right = false;

        for (int i = 0; i < 3; i++)
        {
            if (leftSide[i])// left
            { 
                if (!left)
                {
                    left = true;
                    leftMeshTriangle.Vertices[0] = _triangle.Vertices[i];
                    leftMeshTriangle.Vertices[1] = leftMeshTriangle.Vertices[0];

                    leftMeshTriangle.Uvs[0] = _triangle.Uvs[i];
                    leftMeshTriangle.Uvs[1] = leftMeshTriangle.Uvs[0];

                    leftMeshTriangle.Normals[0] = _triangle.Normals[i];
                    leftMeshTriangle.Normals[1] = leftMeshTriangle.Normals[0];
                }
                else
                {
                    leftMeshTriangle.Vertices[1] = _triangle.Vertices[i];
                    leftMeshTriangle.Normals[1] = _triangle.Normals[i];
                    leftMeshTriangle.Uvs[1] = _triangle.Uvs[i];
                }
            }
            else  // right
            {
                if (!right)
                {
                    right = true;
                    rightMeshTriangle.Vertices[0] = _triangle.Vertices[i];
                    rightMeshTriangle.Vertices[1] = rightMeshTriangle.Vertices[0];

                    rightMeshTriangle.Uvs[0] = _triangle.Uvs[i];
                    rightMeshTriangle.Uvs[1] = rightMeshTriangle.Uvs[0];

                    rightMeshTriangle.Normals[0] = _triangle.Normals[i];
                    rightMeshTriangle.Normals[1] = rightMeshTriangle.Normals[0];
                }
                else
                {
                    rightMeshTriangle.Vertices[1] = _triangle.Vertices[i];
                    rightMeshTriangle.Normals[1] = _triangle.Normals[i];
                    rightMeshTriangle.Uvs[1] = _triangle.Uvs[i];
                }
            }
        }

        // now to find the intersection points between the solo point and the others
        float normalizeDistance;
        float distance;
        Vector3 edgeVector = Vector3.zero; // edge lenght and direction

        edgeVector = rightMeshTriangle.Vertices[0] - leftMeshTriangle.Vertices[0];
        _plane.Raycast(new Ray(leftMeshTriangle.Vertices[0], edgeVector.normalized), out distance);

        normalizeDistance = distance / edgeVector.magnitude;
        Vector3 vertLeft = Vector3.Lerp(leftMeshTriangle.Vertices[0], rightMeshTriangle.Vertices[0], normalizeDistance);
        Vector3 normalLeft = Vector3.Lerp(leftMeshTriangle.Normals[0], rightMeshTriangle.Normals[0], normalizeDistance);
        Vector2 uvLeft = Vector2.Lerp(leftMeshTriangle.Uvs[0], rightMeshTriangle.Uvs[0], normalizeDistance);

        edgeVector = rightMeshTriangle.Vertices[1] - leftMeshTriangle.Vertices[1];
        _plane.Raycast(new Ray(leftMeshTriangle.Vertices[1], edgeVector.normalized), out distance);

        normalizeDistance = distance / edgeVector.magnitude;
        Vector3 vertRight = Vector3.Lerp(leftMeshTriangle.Vertices[1], rightMeshTriangle.Vertices[1], normalizeDistance);
        Vector3 normalRight = Vector3.Lerp(leftMeshTriangle.Normals[1], rightMeshTriangle.Normals[1], normalizeDistance);
        Vector2 uvRight = Vector2.Lerp(leftMeshTriangle.Uvs[1], rightMeshTriangle.Uvs[1], normalizeDistance);

        if (vertLeft != vertRight)
        {
            //tracking newly created points
            _addedVertices.Add(vertLeft);
            _addedVertices.Add(vertRight);
        }

        // make the new triangles
        MeshTriangle currentTriangle;

        Vector3[] updatedVertices = new Vector3[] { leftMeshTriangle.Vertices[0], vertLeft, vertRight };
        Vector3[] updatedNormals = new Vector3[] { leftMeshTriangle.Normals[0], normalLeft, normalRight };
        Vector2[] updatedUvs = new Vector2[] { leftMeshTriangle.Uvs[0], uvLeft, uvRight };

        currentTriangle = new MeshTriangle(updatedVertices, updatedNormals, updatedUvs, _triangle.SubmeshIndex);

        if (updatedVertices[0] != updatedVertices[1] && updatedVertices[0] != updatedVertices[2])
        {
            // check if it is facing the right way
            FacingCheck(currentTriangle);
            // add it
            _leftSide.AddTriangle(currentTriangle);
        }


        updatedVertices = new Vector3[] { leftMeshTriangle.Vertices[0], leftMeshTriangle.Vertices[1], vertRight };
        updatedNormals = new Vector3[] { leftMeshTriangle.Normals[0], leftMeshTriangle.Normals[1], normalRight };
        updatedUvs = new Vector2[] { leftMeshTriangle.Uvs[0], leftMeshTriangle.Uvs[1], uvRight };

        currentTriangle = new MeshTriangle(updatedVertices, updatedNormals, updatedUvs, _triangle.SubmeshIndex);

        if (updatedVertices[0] != updatedVertices[1] && updatedVertices[0] != updatedVertices[2])
        {
            // check if it is facing the right way
            FacingCheck(currentTriangle);
            // add it
            _leftSide.AddTriangle(currentTriangle);
        }


        updatedVertices = new Vector3[] { rightMeshTriangle.Vertices[0], vertLeft, vertRight };
        updatedNormals = new Vector3[] { rightMeshTriangle.Normals[0], normalLeft, normalRight };
        updatedUvs = new Vector2[] { rightMeshTriangle.Uvs[0], uvLeft, uvRight };

        currentTriangle = new MeshTriangle(updatedVertices, updatedNormals, updatedUvs, _triangle.SubmeshIndex);

        if (updatedVertices[0] != updatedVertices[1] && updatedVertices[0] != updatedVertices[2])
        {
            // check if it is facing the right way
            FacingCheck(currentTriangle);
            // add it
            _rightSide.AddTriangle(currentTriangle);
        }


        updatedVertices = new Vector3[] { rightMeshTriangle.Vertices[0], rightMeshTriangle.Vertices[1], vertRight };
        updatedNormals = new Vector3[] { rightMeshTriangle.Normals[0], rightMeshTriangle.Normals[1], normalRight };
        updatedUvs = new Vector2[] { rightMeshTriangle.Uvs[0], rightMeshTriangle.Uvs[1], uvRight };


        currentTriangle = new MeshTriangle(updatedVertices, updatedNormals, updatedUvs, _triangle.SubmeshIndex);

        if (updatedVertices[0] != updatedVertices[1] && updatedVertices[0] != updatedVertices[2])
        {
            // check if it is facing the right way
            FacingCheck(currentTriangle);
            // add it
            _rightSide.AddTriangle(currentTriangle);
        }

    }

    public static void FillCut(List<Vector3> _addedVertices, Plane _plane, GeneratedMesh _leftMesh, GeneratedMesh _rightMesh, int submeshCount)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> polygone = new List<Vector3>();

        // find the needed polygons
        // the cut faces added new vertices by 2 each time to make an edge
        // if two edges contain the same Vector3 point, they are connected

        for (int i = 0; i < _addedVertices.Count; i++)
        {
            // check the edge
            if (!vertices.Contains(_addedVertices[i]))// if it has one, it has this edge
            {
                //new polygon started with this edge
                polygone.Clear();
                polygone.Add(_addedVertices[i]);
                vertices.Add(_addedVertices[i]);
                if (i + 1 < _addedVertices.Count)
                {
                    polygone.Add(_addedVertices[i + 1]);
                    vertices.Add(_addedVertices[i + 1]);
                }

                EvaluatePairs(_addedVertices, vertices, polygone);
                Fill(polygone, _plane, _leftMesh, _rightMesh, submeshCount);
            }
        }
    }

    public static void EvaluatePairs(List<Vector3> _addedVertices, List<Vector3> _vertices, List<Vector3> _polygone)
    {

        bool isDone = false;
        // look for more edges
        while (!isDone)
        {
            isDone = true;
            for (int i = 0; i < _addedVertices.Count; i += 2)
            {
                if (_addedVertices[i] == _polygone[_polygone.Count - 1] && !_vertices.Contains(_addedVertices[i + 1]))
                {
                    isDone = false;
                    _polygone.Add(_addedVertices[i + 1]);
                    _vertices.Add(_addedVertices[i + 1]);
                }
                else if (_addedVertices[i + 1] == _polygone[_polygone.Count - 1] && !_vertices.Contains(_addedVertices[i]))
                {
                    isDone = false;
                    _polygone.Add(_addedVertices[i]);
                    _vertices.Add(_addedVertices[i]);
                }
            }
        }

    }

    // Fill holl when cutting the triangle
    public static void Fill(List<Vector3> _vertices, Plane _plane, GeneratedMesh _leftMesh, GeneratedMesh _rightMesh, int submeshCount)
    {
        // center of the filling
        Vector3 centerPosition = Vector3.zero;

        for (int i = 0; i < _vertices.Count; i++)
        {
            centerPosition += _vertices[i];
        }
        centerPosition = centerPosition / _vertices.Count;

        Vector3 up = new Vector3()
        {
            x = _plane.normal.x,
            y = _plane.normal.y,
            z = _plane.normal.z
        };

        Vector3 left = Vector3.Cross(_plane.normal, _plane.normal);

        Vector3 displacement = Vector3.zero;
        Vector2 uv1 = Vector2.zero;
        Vector2 uv2 = Vector2.zero;

        // go through edges and eliminate by creating triangles with connected edges
        // each new triangle removes 2 edges but creates 1 new edge
        // keep the chain in order
        for (int i = 0; i < _vertices.Count; i++)
        {
            displacement = _vertices[i] - centerPosition;
            uv1 = new Vector2()
            {
                x = .5f + Vector3.Dot(displacement, left),
                y = .5f + Vector3.Dot(displacement, up)
            };

            displacement = _vertices[(i + 1) % _vertices.Count] - centerPosition;
            uv2 = new Vector2()
            {
                x = .5f + Vector3.Dot(displacement, left),
                y = .5f + Vector3.Dot(displacement, up)
            };

            Vector3[] vertices = new Vector3[] { _vertices[i], _vertices[(i + 1) % _vertices.Count], centerPosition };
            Vector3[] normals = new Vector3[] { -_plane.normal, -_plane.normal, -_plane.normal };
            Vector2[] uvs = new Vector2[] { uv1, uv2, new Vector2(0.5f, 0.5f) };
            //Update current triangle
            MeshTriangle currentTriangle = new MeshTriangle(vertices, normals, uvs, submeshCount);

            // check if it is facing the right way
            FacingCheck(currentTriangle);
            // add to left side
            _leftMesh.AddTriangle(currentTriangle);

            normals = new Vector3[] { _plane.normal, _plane.normal, _plane.normal };
            currentTriangle = new MeshTriangle(vertices, normals, uvs, submeshCount);

            // check if it is facing the right way
            FacingCheck(currentTriangle);
            // add to right side
            _rightMesh.AddTriangle(currentTriangle);
        }

    }

    // Check each triangle facing, if not facing right, replace triangle on the right side
    private static void FacingCheck(MeshTriangle _triangle)
    {
        Vector3 crossProduct = Vector3.Cross(_triangle.Vertices[1] - _triangle.Vertices[0], _triangle.Vertices[2] - _triangle.Vertices[0]);
        Vector3 averageNormal = (_triangle.Normals[0] + _triangle.Normals[1] + _triangle.Normals[2]) / 3.0f;
        float dotProduct = Vector3.Dot(averageNormal, crossProduct);
        if (dotProduct < 0)
        {
            // Replace Vertices
            Vector3 temp = _triangle.Vertices[2];
            _triangle.Vertices[2] = _triangle.Vertices[0];
            _triangle.Vertices[0] = temp;

            // Replace Normals
            temp = _triangle.Normals[2];
            _triangle.Normals[2] = _triangle.Normals[0];
            _triangle.Normals[0] = temp;

            // Replace UV
            Vector2 temp2 = _triangle.Uvs[2];
            _triangle.Uvs[2] = _triangle.Uvs[0];
            _triangle.Uvs[0] = temp2;
        }
    }
}
