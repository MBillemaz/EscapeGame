using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropheesManager : MonoBehaviour {


    public static List<TropheeInfo> Trophees = new List<TropheeInfo>();

    // Use this for initialization
    void Start () {
       this.GetTrophees();
        DontDestroyOnLoad(this);
    }
	

    //Get all rewards
    public List<TropheeInfo> GetTrophees()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            Trophees.Add(child.GetComponent<Trophee>().tropheeInfo);
        } 
        return Trophees;
    }



}
