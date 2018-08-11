using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitsVisuals : MonoBehaviour {

    public SpriteRenderer characterVisual;

    public Sprite bartender;
    public Sprite terminator;
    public Sprite rambo;
    public Sprite mercury;

    public Dictionary<string, Sprite> outfits = new Dictionary<string, Sprite>();

    void Start ()
    {
        outfits.Add("Outfit_Bartender", bartender);
        outfits.Add("Outfit_Terminator", terminator);
        outfits.Add("Outfit_Rambo", rambo);
        outfits.Add("Outfit_Mercury", mercury);

        string equipedOutfit = PlayerPrefs.GetString("EquipedOutfit");
        //characterVisual.sprite = outfits<equipedOutfit>();
        
        foreach (KeyValuePair<string, Sprite> key in outfits)
        {
            if (key.Key == equipedOutfit)
                characterVisual.sprite = key.Value;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
