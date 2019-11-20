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
        {
            Scene scene = SceneManager.GetActiveScene();
            Trophee trophee = TropheesManager.Trophees.Find(g => g.GetComponent<Trophee>().Level == scene.name).GetComponent<Trophee>();
            trophee.IsLocked = false;
            Debug.Log(trophee);
            StartCoroutine("ChangeScene", "Menu");
        }
           
    }

    IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(1);
        changeLoaded = true;

        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        yield return null;
    }
}
