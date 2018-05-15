using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 15;

    //[EnumAttribute(), Header("Projectile Delete SFX"), Tooltip("Sound Effect For When Projectile Is Destroyed")]
    //public string destroyClip;

    public float lifeTime = 20;

    private void OnCollisionEnter(Collision coll)
    {
        //AudioManager.instance.PlaySound(destroyClip);

        if (coll.gameObject.CompareTag("Player"))
        {
            //TakeDamage
        }


        Destroy(gameObject);
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
