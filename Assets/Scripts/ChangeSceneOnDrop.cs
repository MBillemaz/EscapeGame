using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDrop : MonoBehaviour
{

    public VRTK_SnapDropZone dropZone; 
    // Use this for initialization
    void Start()
    {
        dropZone.ObjectSnappedToDropZone += CheckItem;
    }

    protected virtual void CheckItem(object sender, SnapDropZoneEventArgs e)
    {
        GameObject item = dropZone.GetCurrentSnappedObject();
        if (item.name == "Epee")
        {
            StartCoroutine("ChangeScene", "Game");
            
        }
    }

    IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        yield return null;
    }
}

