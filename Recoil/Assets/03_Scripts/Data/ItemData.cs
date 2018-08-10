using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public Sprite visual;
    public int cost;
    public int itemIdex;
    public bool isLocked;
}
