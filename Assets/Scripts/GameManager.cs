using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform reSpawnPoint;

    public bool gameComplete = false;

    public float playerDownTime = 5;
    public float levelTime = 120;

   
    public string winningPlayer;

    float currentCoolDown;
    [HideInInspector]
    public float currentTimeLeft;

    public TurfSystem Autumn, Spring, Summer, Winter;
    public List<GameObject> deadPlayers = new List<GameObject>();
    public GameObject[] players;

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
            currentTimeLeft -= Time.deltaTime;
        }
        else
        {
            LevelFinished();
        }
       
        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
        }
        else
        {
            currentCoolDown = 0;
            if (deadPlayers.Count == 0)
                return;

            foreach (var item in deadPlayers.ToArray())
            {
                item.SetActive(true);
                deadPlayers.Remove(item.gameObject);
            }
        }
    }

    private void LevelFinished()
    {
        if (gameComplete)
            return;

       gameComplete = true;
       print(CalculateWinningPlayer());
    }

    public void ReSpawnPlayer(GameObject player)
    {
        player.SetActive(false);
        currentCoolDown = playerDownTime;
        if(!deadPlayers.Contains(player))
        deadPlayers.Add(player);
        player.transform.position = reSpawnPoint.position;
       
    }

}