using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevDebug : MonoBehaviour {

    public PlayerData playerData;

    public void ResterPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        playerData.unlockedOutfits.Clear();
        playerData.unlockedOutfits.Add("Outfit_Office");
        playerData.equipedOutfit = "Outfit_Office";
    }
}
