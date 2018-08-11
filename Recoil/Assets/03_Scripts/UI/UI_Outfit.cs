using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Outfit : MonoBehaviour {

    public int price;
    public Image outline;
    public Image character;
    public GameObject button;
    public PlayerData playerData;
    public Text coinsText;

    bool isUnlocked;

	void Start ()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name))
            CostumesUnlocked();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UnlockCostume();
	}

    void CostumesUnlocked()
    {
        button.SetActive(false);
        character.transform.localScale = Vector3.one * 1.5f;
        character.transform.localPosition = new Vector3(character.transform.localPosition.x, -9.7f, character.transform.localPosition.z);
        character.color = new Color(character.color.r, character.color.g, character.color.b, 1);

        if (PlayerPrefs.GetString("EquipedOutfit") == this.gameObject.name)
        {
            outline.color = new Color(0, 169, 150);
        }

    }

    public void UnlockCostume()
    {
        if (price <= playerData.coins)
        {
            playerData.coins -= price;
            coinsText.text = playerData.coins.ToString();

            isUnlocked = true;

            button.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
            {
                button.SetActive(false);
            });
            character.DOFade(1, 1);
            character.transform.DOScale(Vector3.one * 1.5f, 1f);
            character.transform.DOLocalMoveY(-9.7f, 1);
            outline.DOColor(new Color(0, 169, 150), 1);

            //Update Player Data
            if (!playerData.unlockedOutfits.Contains(this.transform.name))
                playerData.unlockedOutfits.Add(this.transform.name);
            playerData.equipedOutfit = this.gameObject.name;
            PlayerPrefs.SetString(this.gameObject.name, this.gameObject.name);
            PlayerPrefs.SetString("EquipedOutfit", this.gameObject.name);
        }

        
    }
}
