using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform reSpawnPoint;

    public float playerDownTime = 5;
    public float levelTime = 120;

    public string winningPlayer;

    float currentCoolDown;
    float currentTimeLeft;

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

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        currentTimeLeft = levelTime;
    }
    string CalculateWinningPlayer()
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
            if (deadPlayers.Count < 0)
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
