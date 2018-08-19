using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OutfitsSlider : MonoBehaviour {

    public PlayerData playerData;
    public Image fillImage;
    public Text countText;
	
	void Start ()
    {
        SetSlider();
    }

    public void SetSlider()
    {
        float outfitsCount = playerData.unlockedOutfits.Count;
        Debug.Log(outfitsCount);
        float fill = outfitsCount / 27f;
        fillImage.fillAmount = fill;
        countText.text = outfitsCount + "/27";
    }
	
}
