using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurfSystem : MonoBehaviour 
{
    
    public SpriteRenderer currentTile;
    public Sprite playersSeasonTile;
	public List<GameObject> ownedTiles = new List<GameObject>();

    private void Update()
    {
        //CheckTileOwner();
    }

	public void RemoveTile(GameObject old_Tile)
	{
       
                ownedTiles.Remove(old_Tile);

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlatformTile"))
        {
            currentTile = coll.gameObject.GetComponent<SpriteRenderer>();
            if (ownedTiles.Contains(currentTile.gameObject))
            {

              
            }
            else
            {


                currentTile.sprite = playersSeasonTile;
                ownedTiles.Add(currentTile.gameObject);
                currentTile.gameObject.GetComponent<TileStatus>().ChangeOwner(gameObject);
            }
        }

    }

}
