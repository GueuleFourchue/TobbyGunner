using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Shield : MonoBehaviour {

    public bool decreaseFill;

    public Image fillImage;
    public Image shieldIcon;

    Tween myTween;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            DecreaseFillValue(1.5f);
    }

    public void DecreaseFillValue(float duration)
    {
        ActivateShieldSprite();

        //ResetValues
        shieldIcon.DOKill();
        myTween.Kill();
        fillImage.fillAmount = 1;


        myTween = DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, 0, duration).OnComplete(() =>
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
