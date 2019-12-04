using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropheesManager : MonoBehaviour {


    public static List<Trophee> Trophees = new List<Trophee>();

    public GameObject dropPoint;

    // Use this for initialization
    void Start () {
       this.GetTrophees();
       this.GetUnlockedTrophees();
        DontDestroyOnLoad(this);
    }
	

    //Get all rewards
    public List<Trophee> GetTrophees()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            Trophees.Add(child.GetComponent<Trophee>());
        } 
        return Trophees;
    }


    public List<Trophee> GetUnlockedTrophees()
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
                }
            }

        }
        return Trophees;
    }



}
