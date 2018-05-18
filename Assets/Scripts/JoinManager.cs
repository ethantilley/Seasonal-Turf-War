using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour {


    public static JoinManager instance;

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
    
    public GameObject autumn, spring, winter, summer;
    public bool con1B, con2B, con3B, con4B;
    public bool con1Start, con2Start, con3Start, con4Start;


    public bool startButtonActive = true;

    public List<string> playersJoining = new List<string>();
    public GameObject[] playerActive;
    // Use this for initialization
    void Start ()
    {
        autumn = GameObject.Find("Autumn");
        spring = GameObject.Find("Spring");
        winter = GameObject.Find("Winter");
        summer = GameObject.Find("Summer");
        playerActive = GameObject.FindGameObjectsWithTag("Player");
        autumn.SetActive(false);
        spring.SetActive(false);
        winter.SetActive(false);
        summer.SetActive(false);

    }

    private void OnLevelWasLoaded(int level)
    {
        autumn = GameObject.Find("Autumn");
        spring = GameObject.Find("Spring");
        winter = GameObject.Find("Winter");
        summer = GameObject.Find("Summer");
        playerActive = GameObject.FindGameObjectsWithTag("Player");
        autumn.SetActive(false);
        spring.SetActive(false);
        winter.SetActive(false);
        summer.SetActive(false);

        foreach (var item in playerActive)
        {
            if (playersJoining.Contains(item.name))
            {
                item.SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update () {


       

        CheckJoining();

        if (con1B)
        {
            autumn.SetActive(true);
            playersJoining.Add(autumn.name);
        }
       
        if (con2B)
        {
            spring.SetActive(true);
            playersJoining.Add(spring.name);
        }
       
        if (con3B)
        {

            summer.SetActive(true);
            playersJoining.Add(summer.name);

        }

        if (con4B)
        {
            winter.SetActive(true);
            playersJoining.Add(winter.name);  }
       

        if (con1Start)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.instance.GetRandomShuffledScene());
            startButtonActive = false;

            DontDestroyOnLoad(gameObject);
            con1Start = false;

        }

    }
    
    void CheckJoining()
    {
        con1B = Input.GetButtonDown("Controller1_B");


        con2B = Input.GetButtonDown("Controller2_B");
        

        con3B = Input.GetButtonDown("Controller3_B");


        con4B = Input.GetButtonDown("Controller4_B");




        if (Input.GetButtonDown("Controller1_Start") && startButtonActive)
        {
            startButtonActive = false;

            con1Start = true;
        }
       
    }
}
