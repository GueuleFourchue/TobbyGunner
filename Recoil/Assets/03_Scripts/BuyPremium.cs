using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPremium : MonoBehaviour {

    public PlayerData playerData;
    public UI_Outfit ui_Outfit;
    public UI_PremiumWindow ui_PremiumWindow;

	public void BecomePremium()
    {
        PlayerPrefs.SetString("PremiumUser", "PremiumUser");
        playerData.premiumUser = true;
        ui_Outfit.CostumeUnlocked();
        //ui_PremiumWindow.CheckPremium();
    }
}
