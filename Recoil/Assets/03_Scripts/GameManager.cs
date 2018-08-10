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
    bool slowMotion;
    int beginPlayCoins;

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

    public void CharacterDeath(BoxCollider2D collider, Rigidbody2D rb, Animator animator, float shootRecoil)
    {
        collider.enabled = false;

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * shootRecoil * 1.5f, ForceMode2D.Impulse);
        rb.gravityScale *= 2;

        cameraFollow.enabled = false;
        animator.enabled = true;
        animator.Play("Death");

        StartCoroutine(EndLevel());
    }

    public IEnumerator EndLevel()
    {
        playerData.levelGainedcoins = playerData.coins - beginPlayCoins;

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

        blackOverlay.DOFade(0, 0.1f);
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
}
