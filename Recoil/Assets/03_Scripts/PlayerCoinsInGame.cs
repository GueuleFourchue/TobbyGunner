using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinsInGame : MonoBehaviour {

    public PlayerData playerData;

    [Header("Drag & Drop")]
    public UI_InGame UI;
    
    public void AddCoin()
    {
        playerData.coins ++;
        UI.UpdateCoins();
    }
}
