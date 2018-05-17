using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStun : MonoBehaviour
{
    public bool isShoved;
    public GameObject deathParticlePrefab,
        deathPart;
    Vector3 startPoint;

    private void Start()
    {
        startPoint = gameObject.transform.position;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Feet"))
        {
            var par = gameObject.GetComponentInParent<Rigidbody2D>();
            par.velocity = new Vector2(0, 0);
        }

        if (collision.CompareTag("Shove"))
        {
            if (isShoved == false)
            {                
                float magnitude = 10000;
                Vector3 force = transform.position - collision.transform.position;
                // force.Normalize();

                GetComponent<Rigidbody2D>().AddForceAtPosition(force.normalized * magnitude, collision.transform.position);
                isShoved = true;
                StartCoroutine(DontShove());
            }
        }       

        if(collision.CompareTag("DeadZone"))
        {
            if (!deathPart)
            {
                deathPart = Instantiate(deathParticlePrefab, gameObject.transform.position, deathParticlePrefab.transform.rotation);
                Destroy(deathPart, 2);
            }
            
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            if (GameManager.instance != null)
                GameManager.instance.ReSpawnPlayer(gameObject);
        }        
    }
   
    public IEnumerator DontShove()
    {
        yield return new WaitForSeconds(0.3f);
        isShoved = false;
    }
}
