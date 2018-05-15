using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shove : MonoBehaviour
{

    public GameObject shoveTarget;

    // Use this for initialization
    void Start()
    {
        print(transform.parent.name);
        if(transform.parent.name == "Autumn")
        {
            shoveTarget = GameObject.FindGameObjectWithTag("ShoveAut");
        }

        else if(transform.parent.name == "Summer")
        {
            shoveTarget = GameObject.FindGameObjectWithTag("ShoveSum");
        }

        else if (transform.parent.name == "Winter")
        {
            shoveTarget = GameObject.FindGameObjectWithTag("ShoveWin");
        }

        else if (transform.parent.name == "Spring")
        {
            shoveTarget = GameObject.FindGameObjectWithTag("ShoveSpr");
        }

        StartCoroutine(ShoveAction());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, shoveTarget.transform.position, 2 * Time.deltaTime);         
    }

    IEnumerator ShoveAction()
    {        
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
