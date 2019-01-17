using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UI_MenuSwipe : MonoBehaviour
{
    public UI_OutfitsContainer uI_OutfitsContainer;

    public void BeginSwipe()
    {
        uI_OutfitsContainer.BeginSwipe(Input.mousePosition);
    }
    public void EndSwipe()
    {
        uI_OutfitsContainer.EndSwipe();
    }
}
