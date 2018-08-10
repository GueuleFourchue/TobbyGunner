using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float followLerp;
	public float yOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, target.position.y + yOffset, transform.position.z), followLerp);
	}
}
