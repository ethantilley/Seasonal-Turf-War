using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 15;

    //[EnumAttribute(), Header("Projectile Delete SFX"), Tooltip("Sound Effect For When Projectile Is Destroyed")]
    //public string destroyClip;

    public float lifeTime = 20;
    public TurfSystem turf;    

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //AudioManager.instance.PlaySound(destroyClip);

        if (coll.gameObject.CompareTag("Player"))
        {
            //TakeDamage
            print("yeet");
            var magnitude = 5000;
            var force = transform.position - coll.transform.position;
            force.Normalize();
            var rb1 = coll.gameObject.GetComponent<Rigidbody2D>();
            rb1.AddForce(-force * magnitude);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlatformTile"))
        {
            turf.currentTile = coll.gameObject.GetComponent<SpriteRenderer>();
            turf.TileChanger();
        }
    }

    public void Launch(Vector2 direction)
    {
        Destroy(gameObject, lifeTime);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;
    
        rb.AddForce(-direction.normalized * speed);

        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
