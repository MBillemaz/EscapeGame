using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropheesManager : Singleton<TropheesManager> {


    [SerializeField] List<TropheeInfo> trophees;

	
    public List<TropheeInfo> GetTropheesInfo()
    {
        return trophees;
    }

    public TropheeInfo GetTropheeByName(string name)
    {
        return trophees.Find(trophee => trophee.Name == name);
    }

    public TropheeInfo GetTropheeByLevel(string name)
    {
        return trophees.Find(trophee => trophee.Level == name);
    }

    public TropheeInfo GetTropheeByLevelNumber(int number)
    {
        return trophees.Find(trophee => trophee.LevelNumber == number);
    }
}
