using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropheesManager : MonoBehaviour {


    public static List<GameObject> Trophees = new List<GameObject>();
    public static List<GameObject> UnlockedTrophees = new List<GameObject>();

    public GameObject dropPoint;

    // Use this for initialization
    void Start () {
        this.GetTrophees();
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetUnlockedTrophees();
	}

    //Get all rewards
    public List<GameObject> GetTrophees()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            Debug.Log(child.name);
            Trophees.Add(child);
        }
      
        return Trophees;
    }


    public List<GameObject> GetUnlockedTrophees()
    {
        Debug.Log(this.transform.childCount);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;

            if(!child.GetComponent<Trophee>().IsLocked)
            {
                child.SetActive(false);
            }
            else
            {
                child.SetActive(false);
                UnlockedTrophees.Add(child);
            }
        }

        return GetUnlockedTrophees();
    }



}
