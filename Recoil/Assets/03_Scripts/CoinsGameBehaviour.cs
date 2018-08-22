using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinsGameBehaviour : MonoBehaviour {

    public float moveToPlayerSpeed;
    public float spreadForce;
    public Rigidbody2D rb;

    bool moveToPlayer;
    GameObject player;
    float lerpTimeRemaining = 0;

	// Use this for initialization
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FollowPlayer());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (moveToPlayer)
        {
            lerpTimeRemaining += moveToPlayerSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, player.transform.position, lerpTimeRemaining);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerCoinsInGame>().AddCoin();
            Destroy(this.gameObject);
        }
    }

    IEnumerator FollowPlayer()
    { 
        Vector3 randomOriginDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

        float randomSpreadForce = Random.Range(0.7f, 1.3f);
        spreadForce *= randomSpreadForce;
        rb.AddForce(randomOriginDirection * spreadForce, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(0.5f);

        GetComponent<CircleCollider2D>().enabled = true;
        moveToPlayer = true;
    }
}
