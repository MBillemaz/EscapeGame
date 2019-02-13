using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//quitter l(environnement, re
public class save : MonoBehaviour {
    [SerializeField]
    private int maxLevel = 0;
    [SerializeField]
    private int currentLevel = 0;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Level")) {
            maxLevel = PlayerPrefs.GetInt("Level");
            Debug.Log("Niveau chargé avec succes");
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            SceneManager.LoadScene("Continue");

        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P) && PlayerPrefs.GetInt("Level") < 3)
        {
            maxLevel += 1;
            if (maxLevel > PlayerPrefs.GetInt("Level"))
            {
                PlayerPrefs.SetInt("Level", maxLevel);
            }
            currentLevel += 1;
            PlayerPrefs.SetInt("CurrentLevel",currentLevel);
            Debug.Log("Niveau reussi");
        }

        if (Input.GetKeyDown(KeyCode.E) && currentLevel>0)
        {
            currentLevel -= 1;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            Debug.Log("Niveau précedent");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteKey("Level");
            Debug.Log("Reset des niveaux");
        }
    }
}
