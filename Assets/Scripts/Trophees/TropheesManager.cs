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
       this.GetUnlockedTrophees();
    }
	

    //Get all rewards
    public List<GameObject> GetTrophees()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            Trophees.Add(child);
        }
      
        return Trophees;
    }


    public List<GameObject> GetUnlockedTrophees()
    {
        this.transform.GetChild(0).gameObject.GetComponent<Trophee>().IsLocked = false;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            Trophee trophee = child.GetComponent<Trophee>();
            if (trophee)
            {
                if (trophee.IsLocked)
                {
                    child.SetActive(false);
                }
                else
                {
                    child.SetActive(true);
                    UnlockedTrophees.Add(child);
                }
            }

        }

        return UnlockedTrophees;
    }



}
