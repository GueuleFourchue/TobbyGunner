using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuyPopUpButton : MonoBehaviour {

    public UI_Outfit ui_Outfit;

    public void BuyCostume()
    {
        ui_Outfit.BuyCostume();
    }
}
