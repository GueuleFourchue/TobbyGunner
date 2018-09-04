using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colorful;

public class ChunksManager : MonoBehaviour {

    public PlayerData playerData;

    public int nexLevelIndex = 0;
    public int actualLevelIndex = -1;
    public float levelHeight;
    public DeathZone deathZone;

    [Header("Effects & UI")]
    //public BrightnessContrastGamma brightnessContrastGamma;
    public UI_InGame ui_InGame;
    public SpriteRenderer colorBackground;
    float hueValue;

    [Header("Difficulty")]
    public int difficultyLevel = 1;
    public int[] difficultyLevelThresholds;
   

    [Header("Chunks")]
    public GameObject[] chunks_1;
    public GameObject[] chunks_2;
    public GameObject[] chunks_3;
    public GameObject[] chunks_4;
    public GameObject[] chunks_5;
    public GameObject[] chunks_6;

    

    private void Start()
    {
        hueValue = 0.6f;
        AddFirstChunks();
    }

    void AddFirstChunks()
    {
        GameObject.Instantiate(chunks_1[Random.Range(0, chunks_1.Length)], Vector3.zero, Quaternion.identity);
    }

    public void AddChunk()
    {
        deathZone.UpSpeed();

        GameObject newChunk = null;

        
        //DifficultyUp
        foreach (int i in difficultyLevelThresholds)
        {
            if (nexLevelIndex == i)
            {
                difficultyLevel++;
            }
        }

        if (difficultyLevel == 1)
        {
            newChunk = GameObject.Instantiate(chunks_1[Random.Range(0, chunks_1.Length)], new Vector3(0, (nexLevelIndex - 1) * levelHeight, 0), Quaternion.identity);
        }
        if (difficultyLevel == 2)
        {
            newChunk = GameObject.Instantiate(chunks_2[Random.Range(0, chunks_2.Length)], new Vector3(0, (nexLevelIndex - 1) * levelHeight, 0), Quaternion.identity);
        }
        if (difficultyLevel == 3)
        {
            newChunk = GameObject.Instantiate(chunks_3[Random.Range(0, chunks_3.Length)], new Vector3(0, (nexLevelIndex - 1) * levelHeight, 0), Quaternion.identity);;
        }
        if (difficultyLevel == 4)
        {
            newChunk = GameObject.Instantiate(chunks_4[Random.Range(0, chunks_4.Length)], new Vector3(0, (nexLevelIndex - 1) * levelHeight, 0), Quaternion.identity);
        }
        if (difficultyLevel == 5)
        {
            newChunk = GameObject.Instantiate(chunks_5[Random.Range(0, chunks_5.Length)], new Vector3(0, (nexLevelIndex - 1) * levelHeight, 0), Quaternion.identity);
        }
        if (difficultyLevel == 6)
        {
            newChunk = GameObject.Instantiate(chunks_6[Random.Range(0, chunks_6.Length)], new Vector3(0, (nexLevelIndex - 1) * levelHeight, 0), Quaternion.identity);
        }

        //LevelUp
        nexLevelIndex += 1;
        actualLevelIndex = nexLevelIndex - 2;
        newChunk.GetComponent<Chunk>().SetLevelText(nexLevelIndex - 1);

        //UpdateScoreUI
        ui_InGame.UpdateScore(actualLevelIndex);

        /*
        //RedLevelUpEffect
        if (brightnessContrastGamma.ContrastCoeff.x > 0)
            brightnessContrastGamma.ContrastCoeff = new Vector3(brightnessContrastGamma.ContrastCoeff.x - 0.01f, brightnessContrastGamma.ContrastCoeff.y, brightnessContrastGamma.ContrastCoeff.z);
        */

        //Changing color
        hueValue -= 0.02f;
        colorBackground.color = Color.HSVToRGB(hueValue, 0.3f, 0.7f);
        colorBackground.color = new Color(colorBackground.color.r, colorBackground.color.g, colorBackground.color.b, 0.7f);
        

    }

    public void SaveBestLevel()
    {
        if (playerData.bestLevel < actualLevelIndex)
        {
            playerData.bestLevel = actualLevelIndex;
        }
    }

   
}
