using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Shield : MonoBehaviour {

    public float invulnerabilityTimer;
    public bool decreaseFill;

    public Image fillImage;
    public Image shieldIcon;


    public void DecreaseFillValue()
    {
        ActivateShieldSprite();

        DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, 0, invulnerabilityTimer).OnComplete(() =>
        {
            shieldIcon.DOFade(0, 0.2f).OnComplete(() =>
            {
                fillImage.enabled = false;
                shieldIcon.enabled = false;
                fillImage.fillAmount = 1;

            });
        });
    }

    public void ActivateShieldSprite()
    {
        shieldIcon.color = new Color(shieldIcon.color.r, shieldIcon.color.g, shieldIcon.color.b, 1);
        fillImage.enabled = true;
        shieldIcon.enabled = true;

    }
}
