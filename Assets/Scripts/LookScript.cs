using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handles the guns rotation towards the Cursor, 
/// the in game cursor that the player can controll with the right joystick on the controller.
/// </summary>
public class LookScript : MonoBehaviour
{
	public Vector3 cursorPoint;
	public Transform gun;
	public Vector2 direction;
	public float offset;
	public float angle;
	public float ConAngle;

	public float cursorspeed;
	public bool controller;
	public Image imgCusor;
	void Start ()
	{
		
	}

	// Fixed update to make the movment more smoother
	void Update()
	{
		
		

		if (!controller) {
			imgCusor.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);


			direction = cursorPoint - transform.position;
			//set the cursor point to the acual in game Cursor 
			cursorPoint = imgCusor.transform.position;
			// set the direction for the gun to postion between the player and the cursor point
			// Set the angle of the gun to the direction by setting the y and the x of the direction to a radiant than cornvering it to degrees.
			angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			// Set the guns rotation the forward angle position 
			gun.rotation = Quaternion.AngleAxis (angle, Vector3.forward);


			//-----------

		} else {
			// For the right stick controller (aiming)
			// Setting the floats to the axis values for the input on the thright joystick
			float rstickX = Input.GetAxis ("Xbox_RstickX");
			float rstickY = Input.GetAxis ("Xbox_RstickY");
			//Timesing those values by time deltatime (the time i took to complet the last frame) and a adjustable cursor speed 
			rstickX *= Time.deltaTime + cursorspeed;
			rstickY *= Time.deltaTime + cursorspeed;
			direction = new Vector2 (rstickX, rstickY);
			// Moving the cursor position 
			ConAngle = Mathf.Atan2 (rstickY, rstickX) * Mathf.Rad2Deg;
			// Set the guns rotation the forward angle position 
			//uiScript.imgCusor.transform.Translate(0, rsticky, 0);
			imgCusor.transform.position = gun.position + (Vector3)direction.normalized * offset;
			gun.rotation = Quaternion.AngleAxis (ConAngle, Vector3.forward);
		
		}
	}

}
