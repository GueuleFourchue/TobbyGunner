using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ContinueResumeTimer : MonoBehaviour {

    public GameManager gameManager;
    public Text timerText;
    float timerValue = 3;
    public PlayerController playerController;
    float timerThreshold = 2;

    bool activateTimer;
    bool changeText = true;

    public void ResumeTimer()
    {
        timerValue = 3;
        timerThreshold = 2;
        timerText.enabled = true;
        timerText.text = timerValue.ToString();
        activateTimer = true;
        TextAnimation(3f);
    }

    private void Update()
    {
        if (activateTimer)
        {
            timerValue -= Time.deltaTime;
            if (timerValue < timerThreshold)
            {
                TextAnimation(timerThreshold);
                timerThreshold -= 1;
            }
            if (timerValue < 0)
            {
                activateTimer = false;
                playerController.enabled = true;
                gameManager.ResumeAfterContinueTimer();
                timerText.enabled = false;
                this.enabled = false;
            }
        }
    }

    void TextAnimation(float timer)
    {
        timerText.text = timer.ToString();
        transform.DOScale(1.1f, 0.2f).OnComplete(() =>
        {
            transform.DOScale(1f, 0.7f);
        });
        changeText = false;
    }
}
