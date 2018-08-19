using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UI_Outfit : MonoBehaviour {

    public int price;
    public Image outline;
    public Image character;
    public GameObject button;
    public PlayerData playerData;
    public Text coinsText;
    public Text priceText;
    public UI_SelectedOutfit ui_SelectedOutfit;
    public UIAnimations ui_animations;

    bool isUnlocked;

	void Start ()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name))
            CostumesUnlocked();
        priceText.text = price.ToString();
    }
	
    void CostumesUnlocked()
    {
        isUnlocked = true;

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

    public void UnlockCostume()
    {
        if (price <= playerData.coins)
        {
            playerData.coins -= price;
            PlayerPrefs.SetInt("Coins", playerData.coins);
            coinsText.text = playerData.coins.ToString();

            Scene s = SceneManager.GetSceneByName("MAIN");
            GameObject[] mainObjects = s.GetRootGameObjects();
            foreach(GameObject go in mainObjects)
            {
                if (go.name == "Canvas_InGame")
                {
                    go.GetComponent<UI_InGame>().UpdateCoins();
                }
            }

            isUnlocked = true;

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
        }
    }

    public void ChangeCostume()
    {
        if (isUnlocked)
        {
            //Update Player Data
            playerData.equipedOutfit = this.gameObject.name;
            PlayerPrefs.SetString("EquipedOutfit", this.gameObject.name);
            ui_SelectedOutfit.SetNewOutfitOutline(outline, character.sprite, false);
        }
    }

    public void OutfitButtonOnHold()
    {
        if (isUnlocked)
        {
            ui_animations.ButtonOnHold(this.transform);
        }
    }
    public void OutfitButtonOffHold()
    {
        if (isUnlocked)
        {
            ui_animations.ButtonOffHold(this.transform);
        }
    }
}
