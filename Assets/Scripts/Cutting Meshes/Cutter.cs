using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    private string cuttableTag = "Cuttable";
    [SerializeField] Material cutMaterial;
    [SerializeField] bool fill = true;
    [SerializeField] bool addRigibody = false;
    // Use this for initialization
    void Start()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;
        if(victim.tag == cuttableTag)
        {
            this.cutMaterial = this.cutMaterial != null ? this.cutMaterial : collision.collider.GetComponent<MeshRenderer>().sharedMaterials[0];
            Mesh_Cutter.Cut(victim, transform.position, transform.right, this.cutMaterial, fill, addRigibody);
            StopCutting();
        }


    }
    void StopCutting()
    {
        Mesh_Cutter.currentlyCutting = false;
    }



}
