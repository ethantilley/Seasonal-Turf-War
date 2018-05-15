using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : MonoBehaviour {

    public enum Player
    {
        Player1,
        Player2,
        Player3,
        Player4
    }
    public Player selectedPlayer;
    private UnityStandardAssets._2D.PlatformerCharacter2D m_Character;
    public float playerSpeed = 5;
    float x_Move = 0;
    // Use this for initialization
    void Start () {
        m_Character = GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckControllerMovement();


        m_Character.Move(x_Move, false, false);
        
        
    }

    void CheckControllerMovement()
    {
     
        switch (selectedPlayer)
        {
            case Player.Player1:
                x_Move = Input.GetAxis("Controller1_Horizontal");
                break;
            case Player.Player2:
                x_Move = Input.GetAxis("Controller2_Horizontal");
                break;
            case Player.Player3:
                x_Move = Input.GetAxis("Controller3_Horizontal");
                break;
            case Player.Player4:
                x_Move = Input.GetAxis("Controller4_Horizontal");
                break;
            default:
                break;
        }
        
       
        print(GetComponent<Rigidbody2D>().velocity + " : " + x_Move);
    }

}
