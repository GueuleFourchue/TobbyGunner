using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour {

	public float xTpPosition;
	public TrailRenderer trail;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			StartCoroutine(Teleport (other.transform));
		}
	}

	IEnumerator Teleport(Transform player)
	{
		player.position = new Vector3 (xTpPosition, player.position.y, player.position.z);
		trail.enabled = false;
		yield return new WaitForSeconds (trail.time);
		trail.enabled = true;
	}
}
