using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontFixer : MonoBehaviour {
    public Font[] fonts;

	// Use this for initialization
	void Start () {
        foreach (var item in fonts)
        {
            item.material.mainTexture.filterMode = FilterMode.Point;
        }
	}
	
	
}
