using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject {

    public int coins;
    public int bestLevel;
    public string equipedOutfit;
    public List<string> unlockedOutfits = new List<string>();
}
