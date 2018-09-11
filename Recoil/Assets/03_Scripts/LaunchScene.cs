using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour {

    public PlayerData playerData;

	void Start ()
    {
        if (PlayerPrefs.HasKey("PremiumUser"))
            playerData.premiumUser = true;

        StartCoroutine(LoadGUI());
    }

    IEnumerator LoadGUI()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GUI", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene s = SceneManager.GetSceneByName("GUI");
        GameObject[] guiObject = s.GetRootGameObjects();
        guiObject[0].SetActive(true);
        guiObject[1].SetActive(true);
    }
}
