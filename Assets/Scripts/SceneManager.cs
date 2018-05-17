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
    private void OnLevelWasLoaded(int level)
    {
        string name = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (avalibleLevels.Contains(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
        {

            foreach (var item in avalibleLevels.ToArray())
            {
                if (item == name)
                {
                    usedLevels.Add(item);
                    avalibleLevels.Remove(item);
                   
                }
            }            
           
           
        }

    }
    public List<string> avalibleLevels = new List<string>();
    public List<string> usedLevels = new List<string>();
    public string GetRandomShuffledScene ()
    {
        if (avalibleLevels.Count == 0)
        {
            foreach (var item in usedLevels)
            {
                avalibleLevels.Add(item);
            }
            usedLevels.Clear();
        }

        int randScene = Random.Range(0, avalibleLevels.Count);

       

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
