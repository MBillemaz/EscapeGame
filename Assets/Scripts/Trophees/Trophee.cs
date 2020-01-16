using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophee: MonoBehaviour {

    public string Name;
    public string Level;
    public bool IsLocked;
    public int LevelNumber;

    void Start()
    {
        this.Name = this.gameObject.name;
    }

}
