using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text winnerText, timeLeftText;

    public void Update()
    {
        winnerText.text = GameManager.instance.CalculateWinningPlayer();
        timeLeftText.text = GameManager.instance.LevelTimer();
    }

}
