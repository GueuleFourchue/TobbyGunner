using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GUI_Manager : MonoBehaviour
{
    
    public CanvasGroup blackBackgroundCanvas;
    public CanvasGroup buttonsCanvas;
    public CanvasGroup globalCanvasGroup;
    public GameObject GUI_Holder;

    [Header("Variables")]
    public float backgroundFadeTime;

    // Use this for initialization
    void Awake()
    {
        StartCoroutine(LaunchMenu());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LaunchMenu()
    {
        globalCanvasGroup.alpha = 1;
        globalCanvasGroup.interactable = true;
        blackBackgroundCanvas.DOFade(1, backgroundFadeTime);
        yield return new WaitForSeconds(backgroundFadeTime/2);
        buttonsCanvas.DOFade(1, 0.1f);
    }

    public void Restart()
    {
        GUI_Holder.SetActive(false);
        SceneManager.LoadScene("Main");
    }
}
