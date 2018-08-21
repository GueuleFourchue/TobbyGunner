using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    public PlayerData playerData;
    public Canvas canvasInGame;
    public CanvasGroup blackBackground;
    public Image blackOverlay;
    public CameraFollow cameraFollow;
    public OutfitsRewards outfitsRewards;

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

    bool hasProposedContinue;
    bool slowMotion;
    int beginPlayCoins;




	// Use this for initialization
	void Start ()
    {
        originGravityScale = character.GetComponent<Rigidbody2D>().gravityScale;
        beginPlayCoins = playerData.coins;
        StartCoroutine(LoadGUI());
        LoadData();
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

        //Grayscale
        grayscale.enabled = true;
        grayscale.Amount = 0;
        DOTween.To(() => grayscale.Amount, x => grayscale.Amount = x, 0.5f, 0.5f);

        if (!hasProposedContinue)
            StartCoroutine(ContinuePopUp());
        else
            outfitsRewards.StartCoroutine("CheckUnlockedOutfit");
    }

    public IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.5f);

        Scene s = SceneManager.GetSceneByName("GUI");
        GameObject[] guiObject = s.GetRootGameObjects();
        guiObject[0].SetActive(true);
        
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
        foreach(ContinueDeactivate script in monsters)
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
        deathZone.position = deathZoneMinPosition.position;

        //LaunchTimer
        continueResumeTimer.enabled = true;
        continueResumeTimer.ResumeTimer();
        playerController.enabled = false;

        //ImageEffects
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
        playerController.invulnerability = true;
        ui_Shield.DecreaseFillValue();

        //Activate Monsters
        var monsters = FindObjectsOfType<ContinueDeactivate>();
        foreach (ContinueDeactivate script in monsters)
        {
            script.EndTimerStandby();
        }
    }
}
