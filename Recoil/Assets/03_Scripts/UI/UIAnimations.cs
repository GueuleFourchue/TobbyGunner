using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIAnimations : MonoBehaviour {

    public Vector3 scaleButtonPress;

    public GUI_Manager guiManager;
    public CanvasGroup blackOverlayCanvas;

    public PlayerData playerData;
    public Text bestScoreText;

    public CanvasGroup Canvas_Menu;
    public CanvasGroup Canvas_Outfits;

    public Text coinsText;
	
	void Start ()
    {
        bestScoreText.text = "" + playerData.bestLevel;
    }
	
	
    public void ButtonOnHold(Transform buttonTransform)
    {
        buttonTransform.DOKill();
        buttonTransform.DOScale(scaleButtonPress, 0.015f);
    }
    public void ButtonOffHold(Transform buttonTransform)
    {
        buttonTransform.DOKill();
        buttonTransform.DOScale(Vector3.one, 0.2f);
    }

    public void RestartLevel()
    {
        blackOverlayCanvas.DOFade(1, 0.3f).OnComplete(() =>
        {
            guiManager.Restart();
        });
    }

    public void OutfitsWindow()
    {
        Canvas_Menu.DOFade(0, 0.4f).OnComplete(() =>
        {
            Canvas_Menu.gameObject.SetActive(false);
            Canvas_Outfits.gameObject.SetActive(true);
            Canvas_Outfits.DOFade(1, 0.3f);
            coinsText.text = playerData.coins.ToString();
        });
    }

    public void MainWindow()
    {
        Canvas_Outfits.DOFade(0, 0.4f).OnComplete(() =>
        {
            Canvas_Outfits.gameObject.SetActive(false);
            Canvas_Menu.gameObject.SetActive(true);
            Canvas_Menu.DOFade(1, 0.3f);
        });
    }
}
