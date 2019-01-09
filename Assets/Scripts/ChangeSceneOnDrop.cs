using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDrop : MonoBehaviour
{

    void Start()
    {
    }

    public virtual void CheckItem()
    {
        if (gameObject.name == "casque")
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

