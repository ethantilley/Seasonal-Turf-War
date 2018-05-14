using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurfSystem : MonoBehaviour 
{
    
    public SpriteRenderer currentTile;
    public Sprite playersSeasonTile;
	public List<TileStatus> ownedTiles = new List<TileStatus>();

    private void Start()
    {
		InvokeRepeating ("CheckTileOwner", 0, .8f);
    }

	void CheckTileOwner()
	{
		foreach (TileStatus item in ownedTiles)
		{
			if (item.gameObject.GetComponent<TileStatus>().owner != this.gameObject)
			{
				ownedTiles.Remove (item);
			}
		}
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlatformTile"))
        {
            currentTile = coll.gameObject.GetComponent<SpriteRenderer>();
			foreach (TileStatus item in ownedTiles)
            {
				if (item == currentTile.gameObject.GetComponent<TileStatus>())
                {
                    return;
                }
            }

            currentTile.sprite = playersSeasonTile;
			ownedTiles.Add(currentTile.gameObject.GetComponent<TileStatus>());
			currentTile.gameObject.GetComponent<TileStatus> ().owner = this.gameObject;
        }
    }

}
