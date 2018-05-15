using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform reSpawnPoint;

    public float playerDownTime = 5;
    float currentCoolDown;

    public List<GameObject> deadPlayers = new List<GameObject>();

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

    private void Update()
    {
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

    public void ReSpawnPlayer(GameObject player)
    {
        player.SetActive(false);
        currentCoolDown = playerDownTime;
        if(!deadPlayers.Contains(player))
        deadPlayers.Add(player);
        player.transform.position = reSpawnPoint.position;
       
    }

}
