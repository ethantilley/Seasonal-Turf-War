using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStun : MonoBehaviour
{
    public bool isShoved;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Feet"))
        {
            var par = gameObject.GetComponentInParent<Rigidbody2D>();
            par.velocity = new Vector2(0, 0);
        }

        if (collision.CompareTag("Shove" ))
        {
            if (isShoved == false)
            {
                print("hit");
                var magnitude = 5000;
                var force = transform.position - collision.transform.position;
                force.Normalize();
                GetComponent<Rigidbody2D>().AddForce(force * magnitude);
                isShoved = true;
                StartCoroutine(DontShove());
            }
        }

    }

    public IEnumerator DontShove()
    {
        yield return new WaitForSeconds(0.3f);
        isShoved = false;
    }
}
