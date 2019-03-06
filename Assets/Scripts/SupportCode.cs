using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCode : MonoBehaviour {

    private bool isCodeRight = false;
    	
    public void UpdateCodeBool()
    {
        isCodeRight = true;
        
        foreach (var child in gameObject.GetComponentsInChildren<TabletteSupport>())
        {
            if (!child.isValueCorrect)
                isCodeRight = false;                
        }

        CheckCode();
    }
    
    private void CheckCode()
    {
        if (isCodeRight)
            Debug.Log("Bon Code !!");
        else
            Debug.Log("Mauvais Code / Plaquette manquante");
    }
}
