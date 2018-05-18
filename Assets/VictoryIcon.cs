using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryIcon : MonoBehaviour {

    public Sprite autumn, spring, summer, winter;

    private Image image;

	// Use this for initialization
	void Start () {
       
	}

    private void OnEnable()
    {
        image = GetComponent<Image>();
        
        switch (GameManager.instance.CalculateWinningPlayer())
        {
           
            case "Autumn":
                image.sprite = autumn;
                break;
            case "Spring":
                image.sprite = spring;
                break;
            case "Summer":
                image.sprite = summer;
                break;
            case "Winter":
                image.sprite = winter;
                break;

            default:
                Debug.LogWarning("What... ");
                break;
        }
    }
}
