using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script is for player movment, player shooting and 
/// wheather the player enters the enemies vision.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    public float rotationspeed = 100.0f;

    private bool shooting;
    
    public GameObject bulletPrefab;
   
	public Transform bullSpawn;
    
    public Transform gun;

   
    private Rigidbody2D rb;

  
    // Use this for initialization
    void Start()
    {
        //making the cusor invisible so there isnt two(the ingame one and the acual cursor).
        Cursor.visible = false;
        
        //Getting the rigidbody commponent attached to the player
        rb = GetComponent<Rigidbody2D>();
        lookScript = GetComponent<LookScript>();
        anim = GetComponent<Animator>();
        //starting a Coroutine the will loop constantly.
        StartCoroutine(shootCoolDown());
        StartCoroutine(NoAmmoSlowDown());

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //print(Time.timeScale);
        //If the player looses all his health, set him inactive.
        if (health <= 0)
        {
            gameOverUi.SetActive(true);
            Time.timeScale = .5f;
			Cursor.visible = true;
            gameObject.SetActive(false);


        }
		else if (health > 0 && !isEscapeMenuUp && !uiScript.startInputUIActive)
        {
            Time.timeScale = 1;
        }



		//If the player presses the escape key or the B button on the controller.
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
			//if the menu is'nt up
            if (!isEscapeMenuUp)
            {
				//set the Pause menu UI to be active,
                escapeMenuUi.SetActive(true);
				// Stop time,
                Time.timeScale = 0;
				// the menu is up now so set it to true
                isEscapeMenuUp = true;
				// make the cursor visible now do that the player can see.
                Cursor.visible = true;
            }
            else
            {
				// if the menu is up and the player hit one of the keys,
				//Set the pause menu to be actice.
                escapeMenuUi.SetActive(false);
				// Resume normal timescale
                Time.timeScale = 1;
				// Set this to be false 
                isEscapeMenuUp = false;
                //making the cusor invisible so there isnt two(the ingame one and the acual cursor).
                Cursor.visible = false;
            }
        }
        



    }
    //A IEnumerator function that handles, 
    public IEnumerator shootCoolDown()
    {
        if (shooting)
        {
            //firing, 
            FireGun();
            //decreasing ammo.
            Ammo--;
        }
        //then bullet cooldown.
        yield return new WaitForSeconds(.7f);
        //restart the coroutine
        StartCoroutine(shootCoolDown());
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        //stores the use of the xbox trigger as a bool if its greater than 0.
        bool rTrigFloat = Input.GetAxis("Xbox_TiggerRight") > 0f;
        //timesing the players speed by time.deltatime to make the movment time depentant instead of frame dependant.
        translationY *= Time.deltaTime;
        translationX *= Time.deltaTime;
        //acually moves the player
        transform.Translate(0, translationY, 0);
        transform.Translate(translationX, 0, 0);
        rb.transform.eulerAngles = new Vector3(0, 0, 0);
        // transform.rotation = Quaternion.Euler (0, 0, 0);
        bool rTrigActiv = false;
        if (rTrigFloat)
        {
            rTrigActiv = true;
        }
        
        gun.transform.position = new Vector2(gameObject.transform.position.x + .2f, gameObject.transform.position.y - .1f);
        if (translationX < 0)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
            //gun.localScale = new Vector2(3, 3);
        }
        if (translationX > 0)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
            //gun.localScale = new Vector2(3, 3);
        }
       // if the player clicked the mouse or used the controller trigger
        if (Input.GetKey(KeyCode.Mouse0) || rTrigActiv)
        {
			// If there's ammo,
            if (Ammo > 0)
            {
				// set the shooting bool to true.
                shooting = true;

            }
			// if there's no ammo
            else if (Ammo <= 0)
            {
				// Player can't shoot,
                shooting = false;
				// set the noammoshooting bool to true so the player can
                noAmmoShooting = true;
            }
        }
        else
        { 	
			// if the player isn't using the buttons, set the bools to false.
            noAmmoShooting = false;
            shooting = false;
        }
		// Set the animation paramiter to the Horizontal axis so the player can animate
        //anim.SetFloat("MoveX", Input.GetAxis("Horizontal"));
		// Making the world canvas set to the players position for the in game cursor
        worldCanvas.transform.position = transform.position;
    }
	// Coroutine for slowing sown the empty gun sound
    IEnumerator NoAmmoSlowDown()
    {
		
		// wait .7 seconds and 
        yield return new WaitForSeconds(.7f);
		//loop through again.
        StartCoroutine(NoAmmoSlowDown());
    }
	// Function for spawning a bullet
    void FireGun()
    {
		// Move the player to the knockback position attached to the gun.
        gameObject.transform.position = knockBackPoint.position;
		// Spawn the bullet
		Instantiate(bulletPrefab, bullSpawn.position, gun.rotation);


    }


    
}

