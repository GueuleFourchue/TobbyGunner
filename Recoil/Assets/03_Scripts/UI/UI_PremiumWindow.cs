using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PremiumWindow : MonoBehaviour {

    public PlayerData playerData;
    public GameObject isntPremium;
    public GameObject isPremium;

    void Start ()
    {
        if (PlayerPrefs.HasKey("PremiumUser"))
        {
            isPremium.SetActive(true);
            isntPremium.SetActive(false);
        }
        else
        {
            isntPremium.SetActive(true);
            isPremium.SetActive(false);
        }
	}
	
}
