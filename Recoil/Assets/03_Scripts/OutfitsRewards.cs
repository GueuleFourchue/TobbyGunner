using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OutfitsRewards : MonoBehaviour {

    public GameManager gameManager;
    public PlayerData playerData;
    
    public int[] unlockLevels;
    public Sprite[] outfits;
    public bool[] isUnlocked;

    [Header("Animations")]
    public CanvasGroup outfitsRewardsCanvas;
    public Text nameText;
    public Image characterImage;
    public Image locked;
    public Image unlocked;
    public Animator animator;
    public CanvasGroup exitButton;
    public Image blackOverlay;

    bool displayPopUp;
    Vector3 lockerPosition;

    SoundsManager soundsManager;

    private void Start()
    {
        soundsManager = GameObject.FindObjectOfType<SoundsManager>();
        for (int i = 0; i < unlockLevels.Length; i++)
        {
            if (PlayerPrefs.HasKey("IsUnlockedIndex" + i))
            {
                isUnlocked[i] = true;
            }
        }

        lockerPosition = locked.transform.localPosition;
        
    }

    public void CheckUnlockedOutfit()
    {
        for (int i = 0; i < unlockLevels.Length; i++)
        {
            if (unlockLevels[i] <= playerData.bestLevel && isUnlocked[i] == false)
            {
                displayPopUp = true;
                isUnlocked[i] = true;
                PlayerPrefs.SetInt("IsUnlockedIndex" + i, 1);
                StartCoroutine(OutfitReward(i));
            }
        }

        if (!displayPopUp)
            gameManager.StartCoroutine("EndLevel");
    }

    IEnumerator OutfitReward(int index)
    {
        yield return new WaitForSeconds(0.5f);

        soundsManager.PlaySound("UnlockOutfit");

        outfitsRewardsCanvas.gameObject.SetActive(true);
        outfitsRewardsCanvas.DOFade(1f, 0.2f);

        nameText.text = outfits[index].ToString().Replace("S_Outfit_", "").ToUpper();
        characterImage.sprite = outfits[index];

        //locked.transform.DOShakeScale(1, 0.4f, 90, 90);
        locked.gameObject.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(1f);

        locked.transform.localPosition = lockerPosition;
        locked.gameObject.SetActive(false);
        unlocked.gameObject.SetActive(true);
        unlocked.transform.DOScale(1, 0.3f);
        yield return new WaitForSeconds(0.4f);
        unlocked.DOFade(0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        nameText.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        characterImage.DOFade(1, 0.5f);
        characterImage.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(0.5f);
        animator.enabled = true;
        exitButton.gameObject.SetActive(true);
        exitButton.DOFade(1,0.3f);

        displayPopUp = false;
    }

    public void ExitRewardPopUp()
    {
        StartCoroutine(RewardPopOut());
    }

    IEnumerator RewardPopOut()
    {
        outfitsRewardsCanvas.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        outfitsRewardsCanvas.gameObject.SetActive(false);
        blackOverlay.DOFade(0, 0.6f);
        gameManager.StartCoroutine("EndLevel");
    }
}
