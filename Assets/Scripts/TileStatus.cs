using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStatus : MonoBehaviour {

	public GameObject owner;


    public void ChangeOwner(GameObject new_Owner)
    {
        if(owner != null)
            owner.GetComponent<TurfSystem>().RemoveTile(this.gameObject);

        owner = new_Owner;

    }

}
