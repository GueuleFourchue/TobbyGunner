using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingBlock : MonoBehaviour {

    public GameObject deathParticlePrefab;
    public GameObject coinPrefab;

    public int coinsAverageSpawnNumber;
    public int coinsRandomValue;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Destroy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject particle = GameObject.Instantiate(deathParticlePrefab);
        particle.transform.position = transform.position;

        coinsAverageSpawnNumber = coinsAverageSpawnNumber + Random.Range(-coinsRandomValue, coinsRandomValue);
        //SpawnCoins();

        Destroy(particle, 1f);
        Destroy(this.gameObject);
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinsAverageSpawnNumber; i++)
        {
            GameObject coin = GameObject.Instantiate(coinPrefab);
            coin.transform.position = transform.position;
        }
    }
}
