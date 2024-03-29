﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text winnerText, winnerTextBG, timeLeftText, timeLeftTextBG;
    public GameObject gameCompletedPanel;
    public Text gameCompletedPanelWinner,
        gameCompletedPanelWinnerBG;
    public Image blank;
    public Sprite autumn,
        winter,
        summer,
        spring;
    public void Update()
    {
        if (GameManager.instance.gameComplete)
        {
            winnerText.gameObject.SetActive(false);
            timeLeftText.gameObject.SetActive(false);
            winnerTextBG.gameObject.SetActive(false);
            timeLeftTextBG.gameObject.SetActive(false);

            gameCompletedPanel.SetActive(true);
            gameCompletedPanelWinner.text = GameManager.instance.CalculateWinningPlayer();
            gameCompletedPanelWinnerBG.text = gameCompletedPanelWinner.text;
            ChangeWinner();
            JoinManager.instance.startButtonActive = true;
            return;
        }

        winnerText.text = GameManager.instance.CalculateWinningPlayer();
        winnerTextBG.text = winnerText.text;
        timeLeftText.text = GameManager.instance.LevelTimer();
        timeLeftTextBG.text = timeLeftText.text;
    }

    public void ChangeWinner()
    {
        if (gameCompletedPanelWinner.text == "Autumn")
        {
            blank.sprite = autumn;
        }

        else if (gameCompletedPanelWinner.text == "Winter")
        {
            blank.sprite = winter;
        }

        else if (gameCompletedPanelWinner.text == "Summer")
        {
            blank.sprite = summer;
        }

        else if (gameCompletedPanelWinner.text == "Spring")
        {
            blank.sprite = spring;
        }
    }
}
