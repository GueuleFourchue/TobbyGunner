using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyPremium : MonoBehaviour {

    public PlayerData playerData;
    public UI_Outfit ui_Outfit;

	public void BecomePremium()
    {
        PlayerPrefs.SetString("PremiumUser", "PremiumUser");
        playerData.premiumUser = true;
        ui_Outfit.CostumeUnlocked();
        SceneManager.LoadScene("LAUNCH");
    }
}
