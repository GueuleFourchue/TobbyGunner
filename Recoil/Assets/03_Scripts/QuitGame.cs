using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuitGame : MonoBehaviour {

    public GameObject quitCanvas;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SureQuitPopup();
        }
    }

    void SureQuitPopup()
    {
        quitCanvas.SetActive(true);
        quitCanvas.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    }

    public void SureQuitPopout()
    {
        quitCanvas.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
        {
            quitCanvas.SetActive(false);
        });
    }

    public void Quit()
    {
        Application.Quit();
    }
}
