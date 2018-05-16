using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour {

    public GameObject autumn, spring, winter, summer;
    public bool con1B, con2B, con3B, con4B;
    public bool con1Start, con2Start, con3Start, con4Start;

    // Use this for initialization
    void Start () {
    

    }

    private void OnLevelWasLoaded(int level)
    {
        autumn = GameObject.Find("Autumn");
        spring = GameObject.Find("Spring");
        winter = GameObject.Find("Winter");
        summer = GameObject.Find("Summer");
    }

    // Update is called once per frame
    void Update () {


       

        CheckJoining();

        if (con1B)
        {
            autumn.SetActive(true);
        }
        else
        {
            autumn.SetActive(false);
        }

        if (con2B)
        {
            spring.SetActive(true);
        }
        else
        {
            spring.SetActive(false);
        }

        if (con3B)
        {
            winter.SetActive(true);
        }
        else
        {
            winter.SetActive(false);

        }

        if (con4B)
        {
            summer.SetActive(true);
        }
        else
        {
            summer.SetActive(false);

        }


        if (con1Start)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Level");
            DontDestroyOnLoad(gameObject);
            con1Start = false;

        }

    }

    void CheckJoining()
    {
        if (Input.GetButtonDown("Controller1_B"))
            con1B = true;

        if (Input.GetButtonDown("Controller2_B"))
            con2B = true;

        if (Input.GetButtonDown("Controller3_B"))
            con3B = true;

        if (Input.GetButtonDown("Controller4_B"))
            con4B = true;



        if (Input.GetButtonDown("Controller1_Start") && con1B)
            con1Start = true;

        if (Input.GetButtonDown("Controller2_Start") && con2B)
            con2Start = true;

        if (Input.GetButtonDown("Controller3_Start") && con3B)
            con3Start = true;

        if (Input.GetButtonDown("Controller4_Start") && con4B)
            con4Start = true;
    }
}
