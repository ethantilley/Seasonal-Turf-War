using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text winnerText, timeLeftText;
    public GameObject gameCompletedPanel;
    public Text gameCompletedPanelWinner;
    public void Update()
    {
        if (GameManager.instance.gameComplete)
        {
            winnerText.gameObject.SetActive(false);
            timeLeftText.gameObject.SetActive(false);

            gameCompletedPanel.SetActive(true);
            gameCompletedPanelWinner.text = GameManager.instance.CalculateWinningPlayer();

            JoinManager.instance.startButtonActive = true;
            return;
        }

        winnerText.text = GameManager.instance.CalculateWinningPlayer();
        timeLeftText.text = GameManager.instance.LevelTimer();
    }

}
