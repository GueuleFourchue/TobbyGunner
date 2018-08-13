using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_SelectedOutfit : MonoBehaviour {

    public Color unlockedOutfitOutlineColor;
    public Color selectedOutfitOutlineColor;
    public Image tobbyPreview;

    [HideInInspector]
    public Image currentOutfitOutline;
    public Dictionary<string, Sprite> outfits = new Dictionary<string, Sprite>();


    void Start ()
    {
        SetSelectedOutfitOutline();
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    void SetSelectedOutfitOutline()
    {
        foreach(Transform t in this.transform)
        {
            if (PlayerPrefs.GetString("EquipedOutfit") == t.name)
            { 
                t.GetComponent<Image>().color = selectedOutfitOutlineColor;
                currentOutfitOutline = t.GetComponent<Image>();
                StartSetPreviewOutfit(t);
            }
        }
    }

    public void SetNewOutfitOutline(Image image, Sprite tobby)
    {
        currentOutfitOutline.DOColor(unlockedOutfitOutlineColor, 1);
        image.DOColor(selectedOutfitOutlineColor,1);
        currentOutfitOutline = image;
        ChangeOutfitAnim(tobby);
    }

    void ChangeOutfitAnim(Sprite tobbySprite)
    {
        tobbyPreview.transform.GetComponent<Animator>().enabled = true;
        tobbyPreview.sprite = tobbySprite;
    }

    public void StartSetPreviewOutfit(Transform visualParent)
    {
        tobbyPreview.sprite = visualParent.Find("Visual").GetComponent<Image>().sprite;
    }
}
