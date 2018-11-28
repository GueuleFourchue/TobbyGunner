using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Advertisements;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public PlayerData playerData;
    public Canvas canvasInGame;
    public CanvasGroup blackBackground;
    public Image blackOverlay;
    public CameraFollow cameraFollow;
    public OutfitsRewards outfitsRewards;
    public SpriteRenderer charaSprite;
    public GameObject kingSparkles;

    [Header("Continue")]
    public PlayerController playerController;
    public Transform deathZone;
    public Transform deathZoneMinPosition;
    public Transform character;
    public CanvasGroup continueCanvas;
    public Animator continueAnimator;
    public ContinueResumeTimer continueResumeTimer;
    public UI_Shield ui_Shield;
    public Colorful.Grayscale grayscale;
    Vector3 characterDeathPosition;
    float originGravityScale;
    public System.Action OnLevelEnd;

    bool hasProposedContinue;
    public bool HasProposedContinue
    {
        get
        {
            return hasProposedContinue;
        }
    }
    bool slowMotion;
    int beginPlayCoins;




    // Use this for initialization
    void Start()
    {
        audioMixer.DOSetFloat("LowpassFrequency", 22000f, 3f);
        audioMixer.DOSetFloat("MusicVolume", 0, 3f);

        OnLevelEnd += () => { };
        originGravityScale = character.GetComponent<Rigidbody2D>().gravityScale;
        beginPlayCoins = playerData.coins;
        blackOverlay.DOFade(0, 0.2f);
        StartCoroutine(LoadGUI());
        LoadData();

        if (playerData.equipedOutfit == "Outfit_King")
            kingSparkles.SetActive(true);
    }
    void LoadData()
    {
        playerData.coins = PlayerPrefs.GetInt("Coins");
        playerData.bestLevel = PlayerPrefs.GetInt("BestLevel");
    }

    public void CharacterDeath(Transform character, BoxCollider2D collider, Rigidbody2D rb, Animator animator, float shootRecoil)
    {
        characterDeathPosition = character.position;

        collider.enabled = false;

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * shootRecoil * 1.5f, ForceMode2D.Impulse);
        rb.gravityScale *= 2;

        cameraFollow.enabled = false;
        animator.enabled = true;
        animator.Play("Death");

        if (Advertisement.IsReady("rewardedVideo") && !hasProposedContinue && GetComponent<ChunksManager>().actualLevelIndex > PlayerPrefs.GetInt("BestLevel") * .7f)
            StartCoroutine(ContinuePopUp());
        else
            StartCoroutine(DelayOutfitsReward(0.5f));
    }

    IEnumerator DelayOutfitsReward(float delay)
    {
        yield return new WaitForSeconds(delay);
        outfitsRewards.StartCoroutine("CheckUnlockedOutfit");
    }

    public IEnumerator EndLevel()
    {
        audioMixer.DOSetFloat("LowpassFrequency", 4000f, 1.5f);
        audioMixer.DOSetFloat("MusicVolume", -10, 1.5f);

        OnLevelEnd();
        yield return new WaitForSeconds(0.5f);

        Scene s = SceneManager.GetSceneByName("GUI");
        GameObject[] guiObject = s.GetRootGameObjects();
        guiObject[0].SetActive(true);
        guiObject[1].SetActive(true);

        blackBackground.DOFade(1, 0.3f);
    }

    IEnumerator LoadGUI()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GUI", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Coins", playerData.coins);
        PlayerPrefs.SetInt("BestLevel", playerData.bestLevel);
    }

    IEnumerator ContinuePopUp()
    {
        yield return new WaitForSeconds(1.1f);
        continueCanvas.gameObject.SetActive(true);
        continueCanvas.DOFade(1, 0.2f);
        blackOverlay.DOFade(0.8f, 0.1f);
    }

    public void ExitContinue()
    {
        StartCoroutine(ContinuePopOut());
    }

    IEnumerator ContinuePopOut()
    {
        outfitsRewards.CheckUnlockedOutfit();

        continueCanvas.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        continueCanvas.gameObject.SetActive(false);
        blackOverlay.DOFade(0, 0.6f);
    }

    //After Watching Ad
    public void Continue()
    {
        hasProposedContinue = true;

        //Monsters in Standby during timer
        var monsters = FindObjectsOfType<ContinueDeactivate>();
        foreach (ContinueDeactivate script in monsters)
        {
            script.TimerStandby();
        }

        continueCanvas.gameObject.SetActive(false);
        continueCanvas.alpha = 0;
        blackOverlay.DOFade(0, 0.1f);

        //Reset Character
        character.GetComponent<Animator>().Rebind();
        playerController.enabled = true;
        character.position = characterDeathPosition;
        character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        character.GetComponent<Rigidbody2D>().gravityScale = 0;
        cameraFollow.enabled = true;

        //Invu
        playerController.YellowColor();
        ui_Shield.ActivateShieldSprite();

        //Reset Big Monster
        deathZone.position = new Vector3(0, deathZoneMinPosition.position.y, 0);

        //LaunchTimer
        continueResumeTimer.enabled = true;
        continueResumeTimer.ResumeTimer();
        playerController.enabled = false;

        //ImageEffects
        grayscale.enabled = true;
        grayscale.Amount = 0.5f;
        DOTween.To(() => grayscale.Amount, x => grayscale.Amount = x, 0f, 2.5f).OnComplete(() =>
        {
            grayscale.enabled = false;
        });

    }

    public void ResumeAfterContinueTimer()
    {
        //Character
        character.GetComponent<Rigidbody2D>().gravityScale = originGravityScale;
        character.GetComponent<BoxCollider2D>().enabled = true;
        playerController.Invulnerability();

        //Activate Monsters
        var monsters = FindObjectsOfType<ContinueDeactivate>();
        foreach (ContinueDeactivate script in monsters)
        {
            script.EndTimerStandby();
        }
    }
}
