using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AdButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisableAnimator(Animator animator)
    {
        animator.enabled = false;
    }
    public void EnableAnimator(Animator animator)
    {
        animator.enabled = true;
    }
}
