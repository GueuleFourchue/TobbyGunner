using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public Transform deathZoneMinPosition;
    public float moveSpeed;
    public float moveSpeedIncrease;
    public float maxMoveSpeed;

	
	// Update is called once per frame
	void Update ()
    {
        AutoMove();

        if (transform.position.y < deathZoneMinPosition.position.y)
        {
            transform.position = new Vector3(0, deathZoneMinPosition.position.y + 0.1f, 0);
        }
    }

    void AutoMove()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Death();
        }
    }

    public void UpSpeed()
    {
        if (moveSpeed < maxMoveSpeed)
            moveSpeed += moveSpeedIncrease;
    }
}
