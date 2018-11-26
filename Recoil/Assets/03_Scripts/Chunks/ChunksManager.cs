using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colorful;
using System.Linq;
using DG.Tweening;

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
    int currentChunckIndex;
    int random;
    List<GameObject> chunks = new List<GameObject>();

    AudioSource music;

    private void Start()
    {
        hueValue = 0.6f;
        AddFirstChunks();
        music = GameObject.Find("SoundsManager").GetComponent<AudioSource>();
    }

    void AddFirstChunks()
    {
        random = Random.Range(0, chunks_1.Length);
        GameObject.Instantiate(chunks_1[random], Vector3.zero, Quaternion.identity);
        currentChunckIndex = random;
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
            chunks = chunks_1.ToList();
            chunks.RemoveAt(currentChunckIndex);
            random = Random.Range(0, chunks.Capacity - 1);
        }
        if (difficultyLevel == 2)
        {
            chunks = chunks_2.ToList();
            chunks.RemoveAt(currentChunckIndex);
            random = Random.Range(0, chunks.Capacity - 1);
        }
        if (difficultyLevel == 3)
        {
            chunks = chunks_3.ToList();
            chunks.RemoveAt(currentChunckIndex);
            random = Random.Range(0, chunks.Capacity - 1);
        }
        if (difficultyLevel == 4)
        {
            chunks = chunks_4.ToList();
            chunks.RemoveAt(currentChunckIndex);
            random = Random.Range(0, chunks.Capacity - 1);
        }
        if (difficultyLevel == 5)
        {
            chunks = chunks_5.ToList();
            chunks.RemoveAt(currentChunckIndex);
            random = Random.Range(0, chunks.Capacity - 1);
        }

        //Instantiate
        newChunk = GameObject.Instantiate(chunks[random], new Vector3(0, (nexLevelIndex - 1) * levelHeight, 0), Quaternion.identity);
        currentChunckIndex = random;

        //LevelUp
        nexLevelIndex += 1;
        actualLevelIndex = nexLevelIndex - 2;
        newChunk.GetComponent<Chunk>().SetLevelText(nexLevelIndex - 1);

        //UpdateScoreUI
        ui_InGame.UpdateScore(actualLevelIndex);


        //Changing color
        if (hueValue > -0.13f)
            hueValue -= 0.02f;

        colorBackground.color = Color.HSVToRGB(hueValue, 0.45f, 0.7f);
        colorBackground.color = new Color(colorBackground.color.r, colorBackground.color.g, colorBackground.color.b, 0.7f);

        if(actualLevelIndex > 5)
            DOTween.To(() => music.pitch, x => music.pitch = x, music.pitch += 0.01f, 1f);
    }

    public void SaveBestLevel()
    {
        if (playerData.bestLevel < actualLevelIndex)
        {
            playerData.bestLevel = actualLevelIndex;
        }
    }

   
}
