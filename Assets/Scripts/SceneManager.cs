using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {


    public void LoadLevel (string sceneName)
    {
        if (sceneName != "Exit")
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        else
            Application.Quit();
    }



}
