using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {


    public static SceneManager instance;
  
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
    }

    public List<string> avalibleLevels = new List<string>();
    private List<string> usedLevels = new List<string>();
    public string GetRandomShuffledScene ()
    {
        if (avalibleLevels.Count == 0)
        {
            avalibleLevels = usedLevels;
            usedLevels.Clear();
        }

        int randScene = Random.Range(0, avalibleLevels.Count);

        avalibleLevels.Remove(avalibleLevels[randScene]);
        usedLevels.Add(avalibleLevels[randScene]);


        return avalibleLevels[randScene];
    }

    public void LoadLevel (string sceneName)
    {
        if (sceneName != "Exit")
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        else
            Application.Quit();
    }



}
