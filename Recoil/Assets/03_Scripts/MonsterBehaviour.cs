using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterBehaviour : MonoBehaviour {

    public float health;
    public float horizontalMoveSpeed;
    public GameObject deathParticlePrefab;
    public SpriteRenderer monsterSprite;
    public SpriteRenderer targetFeedbackSprite;
    public GameObject coinPrefab;
    Rigidbody2D rb;

    [Header("Coins")]
    public int coinsAverageSpawnNumber;
    public int coinsRandomValue;

    [Header("Grounded")]
    public Transform rayTransform;
    public LayerMask groundLayer;

    Vector3 monsterScale;
    bool isDead;
    bool isGoingRight;
    bool canChangeDirection  = true;

    SoundsManager soundsManager;

    void Start ()
    {
        monsterScale = monsterSprite.transform.localScale;
        soundsManager = GameObject.Find("SoundsManager").GetComponent<SoundsManager>();
    }
	
	void Update ()
    {
		if (health <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Death());
        }

        MoveHorizontal();
	}

    public void TakesDamage(float damage)
    {
        health -= damage;
        monsterSprite.transform.DOKill();
        monsterSprite.transform.DOScaleY(monsterScale.y * 1.2f, 0.1f).OnComplete(() =>
        {
            monsterSprite.transform.DOScaleY(monsterScale.y, 0.1f);
        });
    }

    IEnumerator Death()
    {
        //RemoveMonsterFromShootingList
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.shootTargets.Remove(this.transform);
        //

        
        yield return new WaitForSeconds(0.07f);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.07f);

        soundsManager.PlaySound("MonsterExplosion");
        GameObject particle = GameObject.Instantiate(deathParticlePrefab);
        particle.transform.position = transform.position;

        coinsAverageSpawnNumber = coinsAverageSpawnNumber + Random.Range(-coinsRandomValue, coinsRandomValue);
        SpawnCoins();

        targetFeedbackSprite.enabled = false;
        monsterSprite.DOFade(0, 0.03f);
        Destroy(particle, 1f);
        Destroy(this.gameObject, 1f);
    }

    private void MoveHorizontal()
    {
        if (isGoingRight)
            transform.Translate(Vector3.right * Time.deltaTime * horizontalMoveSpeed);
        else
            transform.Translate(-Vector3.right * Time.deltaTime * horizontalMoveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bound"))
        {
            StartCoroutine(ChangeDirection());
        }
    }

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(rayTransform.position, -Vector2.up, 1f, groundLayer))
        {
            
        }
        else
        {
            if (canChangeDirection)
            {
                StartCoroutine(ChangeDirection());
                canChangeDirection = false;
            }
        }
    }

    IEnumerator ChangeDirection()
    {
        isGoingRight = !isGoingRight;

        if (transform.localScale.x != 1)
        {
            transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        yield return new WaitForSeconds(0.5f);
        canChangeDirection  = true;
    }

    void SpawnCoins()
    {
        for (int i = 0; i< coinsAverageSpawnNumber; i++)
        {
            GameObject coin = GameObject.Instantiate(coinPrefab);
            coin.transform.position = transform.position;
        }
    }
}
