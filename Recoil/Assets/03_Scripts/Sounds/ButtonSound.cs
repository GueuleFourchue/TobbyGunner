using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {

    SoundsManager soundsManager;

	void Start ()
    {
        soundsManager = GameObject.FindObjectOfType<SoundsManager>();
    }
	
	public void ClassicButtonSubmitSound()
    {
        soundsManager.PlaySound("ClassicButton");
    }
    public void BackButtonSound()
    {
        soundsManager.PlaySound("BackButton");
    }
    public void BuyButtonSound()
    {
        soundsManager.PlaySound("BuyButton");
    }
}
