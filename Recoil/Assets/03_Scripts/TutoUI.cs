using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutoUI : MonoBehaviour {

    public Image blackOverlay;
    public GameObject screen01;
    public GameObject screen02;
    public GameObject screen03;

    private void Start()
    {
        blackOverlay.DOFade(0, 3f);
    }

    public void NextScreen(int nextScreen)
    {
        if (nextScreen == 2)
        {
            screen02.SetActive(true);
            screen01.SetActive(false);
        }
        if (nextScreen == 3)
        {
            screen03.SetActive(true);
            screen02.SetActive(false);
        }
    }
    public void LoadGame()
    {
        blackOverlay.DOFade(1, 1.5f).OnComplete(() =>
        {
            SceneManager.LoadScene("MAIN");
        });
    }
}
