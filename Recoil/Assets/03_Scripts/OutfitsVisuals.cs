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
    public Sprite obiwan;
    public Sprite indiana;
    public Sprite doc;
    public Sprite jack;
    public Sprite gandalf;
    public Sprite terminator;
    public Sprite rambo;
    public Sprite joker;
    public Sprite king;



    public Dictionary<string, Sprite> outfits = new Dictionary<string, Sprite>();

    void Start ()
    {
        //Set Character Outfit
        outfits.Add("Outfit_Bartender", bartender);
        outfits.Add("Outfit_Policeman", policeman);
        outfits.Add("Outfit_Radioactive", radioactive);
        outfits.Add("Outfit_Cook", cook);
        outfits.Add("Outfit_Pilote", pilote);
        outfits.Add("Outfit_Military", military);
        outfits.Add("Outfit_Groom", groom);
        outfits.Add("Outfit_Surgeon", surgeon);
        outfits.Add("Outfit_Office", office);

        outfits.Add("Outfit_Obiwan Kenobi", obiwan);
        outfits.Add("Outfit_Indiana Jones", indiana);
        outfits.Add("Outfit_Doc", doc);
        outfits.Add("Outfit_Jack Sparrow", jack);
        outfits.Add("Outfit_Gandalf", gandalf);
        outfits.Add("Outfit_Terminator", terminator);
        outfits.Add("Outfit_Rambo", rambo);
        outfits.Add("Outfit_Joker", joker);
        outfits.Add("Outfit_King", king);

        string equipedOutfit = PlayerPrefs.GetString("EquipedOutfit");
        //characterVisual.sprite = outfits<equipedOutfit>();
        
        foreach (KeyValuePair<string, Sprite> key in outfits)
        {
            if (key.Key == equipedOutfit)
                characterVisual.sprite = key.Value;
        }

    }

}
