using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTarget : MonoBehaviour {

    public PlayerController playerController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            if (!playerController.shootTargets.Contains(collision.transform))
            {
                playerController.shootTargets.Add(collision.transform);
                collision.transform.GetChild(0).Find("TargetedFeedback").gameObject.SetActive(true);
                
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            if (playerController.shootTargets.Contains(collision.transform))
            {
                playerController.shootTargets.Remove(collision.transform);
                collision.transform.GetChild(0).Find("TargetedFeedback").gameObject.SetActive(false);
            }
                
        }
    }
}
