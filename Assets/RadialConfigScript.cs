using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadialConfigScript : MonoBehaviour
{
    // Use this for initialization

    // Update is called once per frame
    public void SwitchScene(string name)
    {
        if (name == "Exit")
        {
            StartCoroutine("ChangeScene", "Menu");
        }
        else if (name == "Save")
        {
            Debug.Log("SAVE");
        }
        else if (name == "Help")
        {
            Debug.Log("HELP");
        }
    }

    IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        yield return null;
    }
}