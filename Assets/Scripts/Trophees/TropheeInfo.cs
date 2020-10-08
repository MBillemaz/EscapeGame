using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Trophee", menuName ="Trophee")]
public class TropheeInfo: ScriptableObject {

    public string Name;
    public string Level;
    public bool IsLocked;
    public int LevelNumber;

}
