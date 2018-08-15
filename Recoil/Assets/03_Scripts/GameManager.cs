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

    [Header ("Continue")]
    public Transform deathZone;
    public Transform deathZoneMinPosition;
    public Transform character;
    public CanvasGroup continueCanvas;
    public Animator continueAnimator;
    Vector3 characterDeathPosition;
    public ContinueResumeTimer continueResumeTimer;

    bool slowMotion;
    int beginPlayCoins;

    //Continue


	// Use this for initialization
	void Start ()
    {
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

        //StartCoroutine(EndLevel());
        StartCoroutine(ContinuePopUp());
    }

    public IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1);

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
        continueCanvas.DOFade(1, 0.4f);
        blackOverlay.DOFade(0.8f, 0.1f);
    }

    //After Watching Ad
    public void Continue()
    {
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
        character.GetComponent<PlayerController>().enabled = true;
        character.position = characterDeathPosition;
        character.GetComponent<BoxCollider2D>().enabled = true;
        character.GetComponent<Rigidbody2D>().gravityScale /= 2;
        cameraFollow.enabled = true;

        //Reset Big Monster
        deathZone.position = deathZoneMinPosition.position;

        //LaunchTimer
        continueResumeTimer.enabled = true;
        continueResumeTimer.ResumeTimer();
        character.GetComponent<PlayerController>().enabled = false;
    }

    public void ResumeAfterContinueTimer()
    {
        //Activate Monsters
        var monsters = FindObjectsOfType<ContinueDeactivate>();
        foreach (ContinueDeactivate script in monsters)
        {
            script.EndTimerStandby();
        }
    }
}
