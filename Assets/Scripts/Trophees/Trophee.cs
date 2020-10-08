using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophee : MonoBehaviour {


    public TropheeInfo tropheeInfo;

    private void Update()
    {
        this.gameObject.SetActive(!tropheeInfo.IsLocked);
    }
}
