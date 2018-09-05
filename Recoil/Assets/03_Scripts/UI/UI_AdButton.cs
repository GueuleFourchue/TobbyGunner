using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UI_AdButton : MonoBehaviour
{
    public GameManager gameManager;
    private int count = 0;

    public void DisableAnimator(Animator animator)
    {
        animator.enabled = false;
    }
    public void EnableAnimator(Animator animator)
    {
        animator.enabled = true;
    }

    public void ShowRewardedVideo()
    {
        Time.timeScale = 0;
        ShowOptions adOption = new ShowOptions();
        adOption.resultCallback = (ShowResult res) =>
        {
            count++;
            Debug.Log(count);
            Time.timeScale = 1;
            if (res == ShowResult.Finished)
            {
                count = 0;
                gameManager.Continue();
            }
            else
            {
                if (count > 1)
                {
                    gameManager.ExitContinue();
                }
            }
        };

        Advertisement.Show("rewardedVideo", adOption);
    }
}
