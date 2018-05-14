using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Feet"))
        {
            var par = gameObject.GetComponentInParent<Rigidbody2D>();
            par.velocity = new Vector2 (0, 0);
        }
    }
}
