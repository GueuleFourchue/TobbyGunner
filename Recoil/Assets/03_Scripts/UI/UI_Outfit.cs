﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UI_Outfit : MonoBehaviour {

    public int price;
    public int unlockAtLevel;

    Text unlockLevelText;

    [Header("Swipe")]
    public UI_OutfitsContainer uI_OutfitsContainer;

    [Header ("Drag & Drop")]
    public Image outline;
    public Image character;
    public GameObject button;
    public PlayerData playerData;
    public Text coinsText;
    public Text priceText;
    public GameObject padlock;
    public UI_SelectedOutfit ui_SelectedOutfit;
    public UIAnimations ui_animations;
    public UI_OutfitsSlider ui_OutfitsSlider;

    [Header("Buy Pop Up")]
    public CanvasGroup buyPopUpCanvas;
    public Text buyPopUpText;
    public Text buyPopUpButtonText;
    public UI_BuyPopUpButton ui_BuyPopUpButton;
    public GameObject canBuy;
    public GameObject cantBuyScore;
    public GameObject cantBuyCoins;
    public GameObject cantBuyPremium;
    public Text cantBuyText;

    public bool isBought;
    public bool isUnlocked;
    public bool premiumOutfit;

    [Header("Audio")]
    public AudioMixer audioMixer;

    SoundsManager soundsManager;

	void Start ()
    {
        if (transform.Find("Locked").GetComponentInChildren<Text>() != null)
        {
            unlockLevelText = transform.Find("Locked").GetComponentInChildren<Text>();
            unlockLevelText.text = "" + unlockAtLevel;
        }

        if (PlayerPrefs.HasKey(this.gameObject.name))
            CostumeBought();
        if (PlayerPrefs.GetInt("BestLevel") >= unlockAtLevel)
            CostumeUnlocked();
        if (premiumOutfit && playerData.premiumUser)
            CostumeUnlocked();
        priceText.text = price.ToString();
        soundsManager = GameObject.FindObjectOfType<SoundsManager>();
    }

    public void CostumeUnlocked()
    {
        padlock.SetActive(false);
        character.enabled = true;
        isUnlocked = true;

        if (!PlayerPrefs.HasKey(this.gameObject.name))
            button.SetActive(true);
    }
	
    void CostumeBought()
    {
        isBought = true;

        button.SetActive(false);
        character.transform.localScale = Vector3.one * 1.4f;
        character.transform.localPosition = new Vector3(character.transform.localPosition.x, 10f, character.transform.localPosition.z);
        character.color = new Color(character.color.r, character.color.g, character.color.b, 1);
        if (PlayerPrefs.GetString("EquipedOutfit") == transform.name)
        {
            //
        }
        else
            outline.color = Color.white;
    }

    public void BuyCostume()
    {
        if (price <= playerData.coins)
        {
            playerData.coins -= price;
            PlayerPrefs.SetInt("Coins", playerData.coins);
            coinsText.text = playerData.coins.ToString();

            Scene s;
            if (SceneManager.GetSceneByName("MAIN").isLoaded)
            {
                s = SceneManager.GetSceneByName("MAIN");
            }
            else
            {
                s = SceneManager.GetSceneByName("LAUNCH");
            }
            GameObject[] mainObjects = s.GetRootGameObjects();
            foreach(GameObject go in mainObjects)
            {
                if (go.name == "Canvas_InGame")
                {
                    go.GetComponent<UI_InGame>().UpdateCoins();
                }
            }

            isBought = true;

            //Sound
            StartCoroutine(BuyJingle());

            button.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
            {
                button.SetActive(false);
            });
            
            character.transform.DOScale(Vector3.one * 1.4f, 1f);
            character.transform.DOLocalMoveY(10f, 1).OnComplete(() =>
            {
                character.DOFade(1, 1);
            });

            ui_SelectedOutfit.SetNewOutfitOutline(outline, character.sprite, true);

            //Update Player Data
            if (!playerData.unlockedOutfits.Contains(this.transform.name))
                playerData.unlockedOutfits.Add(this.transform.name);
            playerData.equipedOutfit = this.gameObject.name;
            PlayerPrefs.SetString(this.gameObject.name, this.gameObject.name);
            PlayerPrefs.SetString("EquipedOutfit", this.gameObject.name);

            ui_OutfitsSlider.SetSlider();
        }
        else
        {
            BuyPopUp();
        }
    }

    public void ClickOutfitButton()
    {
        if (!uI_OutfitsContainer.isDraging)
        {
            if (isBought)
                ChangeCostume();
            else
                BuyPopUp();
        }
    }

    public void ChangeCostume()
    {
        //Update Player Data
        playerData.equipedOutfit = this.gameObject.name;
        PlayerPrefs.SetString("EquipedOutfit", this.gameObject.name);
        ui_SelectedOutfit.SetNewOutfitOutline(outline, character.sprite, false);
    }

    void BuyPopUp()
    {
        buyPopUpCanvas.gameObject.SetActive(true);
        buyPopUpCanvas.DOFade(1, 0.1f);

        if (isUnlocked && price <= playerData.coins)
        {
            canBuy.SetActive(true);
            cantBuyScore.SetActive(false);
            cantBuyCoins.SetActive(false);
            cantBuyPremium.SetActive(false);

            ui_BuyPopUpButton.ui_Outfit = this;
            //Text
            string outfitName = transform.name.ToString().Replace("Outfit_", "");
            buyPopUpText.text = outfitName.ToUpper();
            buyPopUpButtonText.text = price.ToString();
        }
        else 
        {
            canBuy.SetActive(false);
            if (isUnlocked)
            {
                cantBuyScore.SetActive(false);
                cantBuyPremium.SetActive(false);
                cantBuyCoins.SetActive(true);
            }
            else if (!premiumOutfit)
            {
                cantBuyText.text = unlockAtLevel.ToString();
                cantBuyScore.SetActive(true);
                cantBuyPremium.SetActive(false);
                cantBuyCoins.SetActive(false);
            }
            else
            {
                cantBuyPremium.SetActive(true);
                cantBuyCoins.SetActive(false);
                cantBuyScore.SetActive(false);
            }
        }
    }


    public void BuyPopUpExit()
    {
        buyPopUpCanvas.DOFade(0, 0.1f).OnComplete(() =>
        {
            buyPopUpCanvas.gameObject.SetActive(false);
        });
    }

    IEnumerator BuyJingle()
    {
        audioMixer.DOSetFloat("MusicVolume", -30, 0.5f);
        soundsManager.PlaySound("BuyNewOutfit");
        yield return new WaitForSeconds(2.5f);
        audioMixer.DOSetFloat("MusicVolume", -5, 1f);
    }
}
