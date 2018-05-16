using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : MonoBehaviour
{

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
    bool jumped;

    float rstickX;
    float rstickY;
    private bool rTrigger;
    private bool rBumper;
    private float cursorspeed = 5;
    Vector2 direction;
    float ConAngle;
    public float offset;
    public float angle;
    public Image imgCusor;
    public Transform gun;
    public GameObject projectilePrefab,
        shovePrefab;
    public float fireRate = 0.5f;

    // Use this for initialization
    void Start()
    {
       
        m_Character = GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(GunCoolDown());
        StartCoroutine(ShoveCoolDown());
    }
    IEnumerator GunCoolDown ()
    {
        if(rTrigger)
        {
            FireProjectile();
            Debug.Log("Hmmmmmmmmmmmmmmm?: 1");

            yield return new WaitForSeconds(fireRate);

        }
        yield return new WaitForSeconds(.2f);
        StartCoroutine(GunCoolDown());
    }
    IEnumerator ShoveCoolDown()
    {
        if (rBumper)
        {
            Shove();
            Debug.Log("Hmmmmmmmmmmmmmmm?: 3");

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(.2f);
        StartCoroutine(ShoveCoolDown());
    }
    private void Update()
    {
        CheckControllerMovement();

        if (imgCusor.transform.position == gameObject.transform.position)
        {
            imgCusor.gameObject.SetActive(false);
        }
        else
        {
            imgCusor.gameObject.SetActive(true);
        }
      
        // imgCusor.transform.rotation = Quaternion.(ConAngle, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Timesing those values by time deltatime (the time i took to complet the last frame) and a adjustable cursor speed 
        rstickX *= Time.deltaTime + cursorspeed;
        rstickY *= Time.deltaTime + cursorspeed;
        direction = new Vector2(rstickX, rstickY);
        // Moving the cursor position 
        ConAngle = Mathf.Atan2(rstickY, rstickX) * Mathf.Rad2Deg;
        // Set the guns rotation the forward angle position 
        //uiScript.imgCusor.transform.Translate(0, rsticky, 0);
        imgCusor.transform.position = gun.position + (Vector3)direction.normalized * offset;
        gun.rotation = Quaternion.AngleAxis(ConAngle, Vector3.forward);

        m_Character.Move(x_Move, false, jumped);

        jumped = false;
   
    }

    void CheckControllerMovement()
    {
        if (GameManager.instance != null && GameManager.instance.gameComplete)
            return;

        switch (selectedPlayer)
        {
            case Player.Player1:
                x_Move = Input.GetAxis("Controller1_Horizontal");

                rstickX = Input.GetAxis("Controller1_RStickX");
                rstickY = Input.GetAxis("Controller1_RStickY");

                rTrigger = Input.GetAxis("Controller1_RightTrigger") > 0f;
                if(!rBumper)
                    rBumper = Input.GetButtonDown("Controller1_X");

                if (!jumped)
                    jumped = Input.GetButtonDown("Controller1_Jump");
                break;
            case Player.Player2:
                x_Move = Input.GetAxis("Controller2_Horizontal");

                rstickX = Input.GetAxis("Controller2_RStickX");
                rstickY = Input.GetAxis("Controller2_RStickY");

                rTrigger = Input.GetAxis("Controller2_RightTrigger") > 0f;
                if (!rBumper)
                    rBumper = Input.GetButtonDown("Controller2_X");

                if (!jumped)
                    jumped = Input.GetButtonDown("Controller2_Jump");
                break;
            case Player.Player3:
                x_Move = Input.GetAxis("Controller3_Horizontal");

                rstickX = Input.GetAxis("Controller3_RStickX");
                rstickY = Input.GetAxis("Controller3_RStickY");

                rTrigger = Input.GetAxis("Controller3_RightTrigger") > 0f;

                if (!rBumper)
                    rBumper = Input.GetButtonDown("Controller3_X");

                if (!jumped)
                    jumped = Input.GetButtonDown("Controller3_Jump");
                break;
            case Player.Player4:
                x_Move = Input.GetAxis("Controller4_Horizontal");

                rstickX = Input.GetAxis("Controller4_RStickX");
                rstickY = Input.GetAxis("Controller4_RStickY");

                rTrigger = Input.GetAxis("Controller4_RightTrigger") > 0f;

                if (!rBumper)
                    rBumper = Input.GetButtonDown("Controller4_X");

                if (!jumped)
                    jumped = Input.GetButtonDown("Controller4_Jump");
                break;
            default:
                break;
        }


        //print(GetComponent<Rigidbody2D>().velocity + " : " + x_Move);
    }

    void FireProjectile()
    {
        Debug.Log("Hmmmmmmmmmmmmmmm?: 2");
        GameObject newProj = Instantiate(projectilePrefab, imgCusor.gameObject.transform.position, gun.rotation);
        var script = newProj.GetComponent<Projectile>();
        script.turf = gameObject.GetComponent<TurfSystem>();
       Vector2 direction = gameObject.transform.position - imgCusor.transform.position;
       newProj.GetComponent<Projectile>().Launch(direction);
    }
    void Shove()
    {
        Debug.Log("Hmmmmmmmmmmmmmmm?: 1");

        Instantiate(shovePrefab, gameObject.transform, false);
        rBumper = false;
    }
}
