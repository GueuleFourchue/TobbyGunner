using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public float directionRandomness;
    public float damage;
	public Rigidbody2D rb;

	[HideInInspector]
	public float bulletSpeed;
	public Vector3 originDirection;
    public bool isSuperShoot;

	int collisionsCount;
	
	// Update is called once per frame
	void Update () 
	{
		if (collisionsCount > 1) 
		{
            Destroy();
		}
	}

	public void Move()
	{
        if (isSuperShoot)
        {
            float xDeviation = Random.Range(-directionRandomness * 2, +directionRandomness * 2);
            float yDeviation = Random.Range(-directionRandomness * 2, +directionRandomness * 2);
            rb.velocity = new Vector3(originDirection.x + xDeviation, originDirection.y + yDeviation, originDirection.z) * bulletSpeed;
        }
        else
        {
            float xDeviation = Random.Range(-directionRandomness, +directionRandomness);
            float yDeviation = Random.Range(-directionRandomness, +directionRandomness);
            rb.velocity = new Vector3(originDirection.x + xDeviation, originDirection.y + yDeviation, originDirection.z) * bulletSpeed;
        }

		Destroy (this.gameObject, 3);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
        if (other.transform.CompareTag("Monster"))
        {
            other.transform.GetComponent<MonsterBehaviour>().TakesDamage(damage);
            Destroy();
        }

		collisionsCount++;
	}

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
