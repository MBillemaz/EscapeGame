using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SupportCode : MonoBehaviour {

    private bool isCodeRight = false;

    private bool changeLoaded = false;
    	
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
        if (isCodeRight && !changeLoaded)
            StartCoroutine("ChangeScene", "Menu");
    }

    IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(1);
        changeLoaded = true;

        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        yield return null;
    }
}
