using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        float fill = outfitsCount / 18f;

        DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, fill, 1);
        //fillImage.fillAmount = fill;
        countText.text = outfitsCount + "/18";
    }
	
}
