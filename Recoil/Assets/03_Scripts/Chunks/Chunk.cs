using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

    public TextMesh textMesh;

    [Header ("CoinBlocks")]
    public GameObject[] coinBlocks;
    public float frequency;

    private void Start()
    {
        float random = Random.Range(0f, 1f);
        if (random <= frequency)
        {
            CoinBlocks();
        }
    }

    public void SetLevelText(int level)
    {
        textMesh.text = "" + level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    void CoinBlocks()
    {
        coinBlocks[Random.Range(0, coinBlocks.Length - 1)].SetActive(true);
    }
}
