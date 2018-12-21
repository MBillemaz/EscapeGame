namespace DefaultNamespace
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    //using VRTK;
    using UnityEngine.SceneManagement;

    public class ConfigRadialMenu : MonoBehaviour
    {
        private string name;
        private string sceneName;
        
       // public VRTK_SnapDropZone dropZone;

        //private void OnChangeScene()
        //{
        //    GameObject item = dropZone.GetCurrentSnappedObject();
        //    if (item.name == name)
        //    {
        //        StartCoroutine("ChangeScene", sceneName);
            
        //    }
        //}
        
        IEnumerator ChangeScene(string scene)
        {
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene(scene, LoadSceneMode.Single);

            yield return null;
        }
    }
}