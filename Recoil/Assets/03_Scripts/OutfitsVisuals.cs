using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitsVisuals : MonoBehaviour {

    public SpriteRenderer characterVisual;

    [Header("Jobs")]
    public Sprite bartender;
    public Sprite policeman;
    public Sprite radioactive;
    public Sprite cook;
    public Sprite pilote;
    public Sprite military;
    public Sprite groom;
    public Sprite surgeon;
    public Sprite office;

    [Header("Celebrities")]
    public Sprite mercury;

    [Header("Heroes")]
    public Sprite terminator;
    public Sprite rambo;
    

    public Dictionary<string, Sprite> outfits = new Dictionary<string, Sprite>();

    void Start ()
    {
        outfits.Add("Outfit_Bartender", bartender);
        outfits.Add("Outfit_Policeman", policeman);
        outfits.Add("Outfit_Radioactive", radioactive);
        outfits.Add("Outfit_Cook", cook);
        outfits.Add("Outfit_Pilote", pilote);
        outfits.Add("Outfit_Military", military);
        outfits.Add("Outfit_Groom", groom);
        outfits.Add("Outfit_Surgeon", surgeon);
        outfits.Add("Outfit_Office", office);

        outfits.Add("Outfit_Mercury", mercury);

        outfits.Add("Outfit_Terminator", terminator);
        outfits.Add("Outfit_Rambo", rambo);

        string equipedOutfit = PlayerPrefs.GetString("EquipedOutfit");
        //characterVisual.sprite = outfits<equipedOutfit>();
        
        foreach (KeyValuePair<string, Sprite> key in outfits)
        {
            if (key.Key == equipedOutfit)
                characterVisual.sprite = key.Value;
        }

    }

}
