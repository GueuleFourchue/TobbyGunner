using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour
{
    public GameObject container;
    public Text text;
    public int gamePlayed;

    public GameManager gameManager;
    public string[] texts;
    private int index;
    // Use this for initialization

    void Start()
    {
        gamePlayed = PlayerPrefs.GetInt("gamePlayed");
        container = transform.GetChild(0).gameObject;
        text = container.GetComponentInChildren<Text>();
        gameManager.OnLevelEnd += OnLevelEnd;
    }

    void OnLevelEnd()
    {
        gamePlayed++;
        PlayerPrefs.SetInt("gamePlayed", gamePlayed);
        index = texts.Length;

        if (gamePlayed % Mathf.Max(Mathf.Round(100 / gamePlayed), 3) == 0 && Advertisement.IsReady())
            StartCoroutine(ShowInterstitialAd());
    }

    IEnumerator ShowInterstitialAd()
    {
        container.SetActive(true);
        for (int i = index - 1; i >= 0; i--)
        {
            text.text = texts[i];
            yield return new WaitForSeconds(.7f);
        }

        ShowOptions adOption = new ShowOptions();
        adOption.resultCallback = (ShowResult res) =>
        {
            container.SetActive(false);
        };
        Advertisement.Show("interstitials", adOption);
    }
}
