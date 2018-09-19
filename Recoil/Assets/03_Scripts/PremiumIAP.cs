using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PremiumIAP : MonoBehaviour
{
    // Use this for initialization
    public void OnPurchaseComplete(Product product)
    {
        Debug.Log(product);
        if (product != null)
        {
            switch (product.definition.id)
            {
                case "premium":
                    Debug.Log("bought");
                    PlayerPrefs.SetInt("premium", 1111);
                    break;
            }
        }
    }
}
