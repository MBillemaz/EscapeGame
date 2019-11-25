using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDrop : MonoBehaviour, SnapActionInterface
{
    public string level;

    public bool hasItem;

    public GameObject dropObject;
    void Start()
    {
    }

    IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        yield return null;
    }

    public void SnapAction(object name)
    {
        Trophee trophee = TropheesManager.Trophees.Find((t) => t.name == name.ToString());
        Debug.Log(trophee);
        if (name.ToString() == trophee.name)
        {
            StartCoroutine("ChangeScene", trophee.Level);
        }
    }

    public void ToggleHasItem()
    {
        hasItem = !hasItem;
    }

    public bool HasItem()
    {
        return hasItem;
    }

    public void setDropObject(GameObject obj)
    {
        dropObject = obj;
    }

    public GameObject DropObject()
    {
        return dropObject;
    }
}

