using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_SelectedOutfit : MonoBehaviour {

    public Color unlockedOutfitOutlineColor;
    public Color selectedOutfitOutlineColor;
    public Image tobbyPreview;
    public CanvasGroup spotLight;
    public Image lightStar1;
    public Image lightStar2;
    public PlayerData playerData;

    [HideInInspector]
    public Image currentOutfitOutline;


    void Start ()
    {
        SetFirstOutfit();
        SetSelectedOutfitOutline();
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

    public void SetNewOutfitOutline(Image image, Sprite tobby, bool newOutfit)
    {
        currentOutfitOutline.DOColor(unlockedOutfitOutlineColor, 0.05f);
        image.DOColor(selectedOutfitOutlineColor, 0.05f);
        currentOutfitOutline = image;
        if (newOutfit)
        {
            StopCoroutine(ChangeNewOutfitAnim(tobby));
            StartCoroutine(ChangeNewOutfitAnim(tobby));
        }
            
        else
            StartCoroutine(ChangeOwnedOutfitAnim(tobby));
    }

    IEnumerator ChangeNewOutfitAnim(Sprite tobbySprite)
    {
        //Reset values
        tobbyPreview.transform.GetComponent<Animator>().Rebind();
        tobbyPreview.color = new Color(tobbyPreview.color.r, tobbyPreview.color.g, tobbyPreview.color.b, 1);
        tobbyPreview.transform.eulerAngles = Vector3.zero;
        tobbyPreview.transform.localScale = Vector3.one;
        spotLight.alpha = 0.4f;

        //Effects
        //Out
        tobbyPreview.transform.DOKill();
        spotLight.DOKill();
        spotLight.DOFade(0, 0.2f);
        tobbyPreview.transform.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        tobbyPreview.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);

        //In
        tobbyPreview.transform.GetComponent<Animator>().enabled = false;
        tobbyPreview.sprite = tobbySprite;
        tobbyPreview.transform.eulerAngles = Vector3.zero;
        tobbyPreview.DOFade(1, 0.1f);
        tobbyPreview.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.3f);
        spotLight.DOFade(0.8f, 1f);

        lightStar1.DOKill();
        lightStar2.DOKill();
        lightStar1.DOFade(1f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        lightStar1.DOFade(0f, 0.4f);
        lightStar2.DOFade(1f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        lightStar2.DOFade(0f, 0.4f);
    }

    IEnumerator ChangeOwnedOutfitAnim(Sprite tobbySprite)
    {
        tobbyPreview.DOKill();
        tobbyPreview.sprite = tobbySprite;
        tobbyPreview.transform.DOScale(0.9f, 0.1f);
        yield return new WaitForSeconds(0.03f);
        tobbyPreview.transform.DOScale(1f, 0.7f).SetEase(Ease.OutElastic);
    }

    public void StartSetPreviewOutfit(Transform visualParent)
    {
        tobbyPreview.sprite = visualParent.Find("Visual").GetComponent<Image>().sprite;
    }

    void SetFirstOutfit()
    {
        if (!PlayerPrefs.HasKey("EquipedOutfit"))
        {
            PlayerPrefs.SetString("Outfit_Office", "Outfit_Office");
            PlayerPrefs.SetString("EquipedOutfit", "Outfit_Office");
            playerData.equipedOutfit = "Outfit_Office";
        }
    }
}
