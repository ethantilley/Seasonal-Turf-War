using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELTETHIS : MonoBehaviour
{
    public GameObject shove;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var pos = gameObject.transform.position;
            Instantiate(shove,gameObject.transform,false);
        }
    }
}
