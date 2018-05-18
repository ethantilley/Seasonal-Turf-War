using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform reSpawnPoint;

    public bool gameComplete = false;

    public float playerDownTime = 5;
    public float levelTime = 120;

    public bool gamePaused = false;
   
    public string winningPlayer;

    float currentCoolDown;
    [HideInInspector]
    public float currentTimeLeft;

    public TurfSystem Autumn, Spring, Summer, Winter;
    public List<GameObject> deadPlayers = new List<GameObject>();
    public GameObject[] players;
    public float currDownTime1, currDownTime2, currDownTime3, currDownTime4;
    public static GameManager instance;
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

    public string LevelTimer()
    {
        if (currentTimeLeft <= 0)
        {
            return "0:00";
        }

        string minutes = Mathf.Floor(currentTimeLeft / 60).ToString();

        string seconds = (currentTimeLeft % 60).ToString("00");

        return string.Format("{0}:{1}", minutes, seconds);
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        currentTimeLeft = levelTime;
    }
    public string CalculateWinningPlayer()
    {
        GameObject test = players[0];
        foreach (var item in players)
        {
            if (item.GetComponent<TurfSystem>().ownedTiles.Count > test.GetComponent<TurfSystem>().ownedTiles.Count)
            {
                test = item;
            }
        }
        return test.name;

    }
    private void Update()
    {
        if (currentTimeLeft > 0)
        {

            if (!gamePaused)
                currentTimeLeft -= Time.deltaTime;
        }
        else
        {
            LevelFinished();
        }

        //--------


        if (currDownTime1 > 0)
        {
            currDownTime1 -= Time.deltaTime;
        }
        else
        {
            currDownTime1 = 0;

            if (deadPlayers.Count == 0)
                return;

            deadPlayers[0].SetActive(true);
         
            deadPlayers.Remove(deadPlayers[0]);
            
        }

        if (currDownTime2 > 0)
        {
            currDownTime2 -= Time.deltaTime;
        }
        else
        {
            currDownTime2 = 0;

            if (deadPlayers.Count < 2)
                return;

            deadPlayers[1].SetActive(true);
            deadPlayers.Remove(deadPlayers[1]);

        }

        if (currDownTime3 > 0)
        {
            currDownTime3 -= Time.deltaTime;
        }
        else
        {
            currDownTime3 = 0;

            if (deadPlayers.Count < 3)
                return;

            deadPlayers[2].SetActive(true);
            deadPlayers.Remove(deadPlayers[2]);

        }

        if (currDownTime4 > 0)
        {
            currDownTime4 -= Time.deltaTime;
        }
        else
        {
            currDownTime4 = 0;

            if (deadPlayers.Count < 4)
                return;

            deadPlayers[3].SetActive(true);
         
            deadPlayers.Remove(deadPlayers[3]);

        }
    }

    private void LevelFinished()
    {
        if (gameComplete)
            return;

        gameComplete = true;

        JoinManager.instance.startButtonActive = true;
    }


    public void ReSpawnPlayer(GameObject player)
    {
        
        CameraController.instance.StartShake(GetComponent<ScreenShake>().properties);
        player.SetActive(false);
        //player.GetComponent<CharacterStun>().currentCoolDown = playerDownTime;
        currentCoolDown = playerDownTime;
        if(!deadPlayers.Contains(player))
            deadPlayers.Add(player);


        switch (deadPlayers.Count)
        {
            case 1:
                currDownTime1 = playerDownTime;
                break;
            case 2:
                currDownTime2 = playerDownTime;
                break;
            case 3:
                currDownTime3 = playerDownTime;
                break;
            case 4:
                currDownTime4 = playerDownTime;
                break;
            default:
                break;
        }
        player.transform.position = reSpawnPoint.position;
        
    }

}