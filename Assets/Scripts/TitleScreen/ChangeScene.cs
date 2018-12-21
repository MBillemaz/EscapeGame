using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public int secToWait;
    public string sceneName;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        coroutine = WaitForScene();
        StartCoroutine(coroutine);
       
	}
	
	// Update is called once per frame
	//void Update () {
 //       var controller = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK.VRTK_ControllerEvents>();

 //       if (controller.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress)){
 //           StopCoroutine(coroutine);
 //           SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
 //       }
	//}

    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(secToWait);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        yield return null;
    }
}
