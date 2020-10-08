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
        TropheeInfo trophee = TropheesManager.Instance.GetTropheeByName(name.ToString());
        if (trophee && name.ToString() == trophee.Name)
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

