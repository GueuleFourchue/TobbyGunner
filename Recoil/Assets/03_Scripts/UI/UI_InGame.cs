using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_InGame : MonoBehaviour {

    public Text coinsText;
    public Text scoreText;
    public PlayerData playerData;

    private void Start()
    {
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        coinsText.text = "" + playerData.coins;
    }

    public void UpdateScore(int level)
    {
        scoreText.text = "" + level;
        scoreText.DOKill();
        DOTween.To(() => scoreText.fontSize, x => scoreText.fontSize = x, 55, 0.1f).OnComplete(() =>
        {
            DOTween.To(() => scoreText.fontSize, x => scoreText.fontSize = x, 40, 0.1f);
        });
    }
}
