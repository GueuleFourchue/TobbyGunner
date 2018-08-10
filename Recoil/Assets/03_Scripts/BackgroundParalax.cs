using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParalax : MonoBehaviour {

    public Rigidbody2D playerRb;
    public Transform spriteFront;
    public Transform spriteBack;

    [Header("Paralax Speed")]
    public float spriteFrontSpeed;
    public float spriteBackSpeed;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerRb.velocity.y < 0)
        {
            spriteFront.Translate(Vector3.right * Time.deltaTime * spriteFrontSpeed);
            spriteBack.Translate(Vector3.right * Time.deltaTime * spriteBackSpeed);
        }
        else if (playerRb.velocity.y > 0)
        {
            spriteFront.Translate(-Vector3.right * Time.deltaTime * spriteFrontSpeed);
            spriteBack.Translate(-Vector3.right * Time.deltaTime * spriteBackSpeed);
        }
	}
}
