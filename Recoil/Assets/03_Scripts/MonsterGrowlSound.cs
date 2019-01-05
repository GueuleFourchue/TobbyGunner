using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterGrowlSound : MonoBehaviour {

    public Transform camera;
    public AudioSource audioSource;

    float offset;
    bool isDecreasing;

    private void Update()
    {
        /*
        offset = camera.transform.position.y - transform.position.y;
        if (Mathf.Abs(offset) > 20 && audioSource.volume!=0 && !isDecreasing)
        {
            audioSource.volume = audioSource.volume - (offset / 20);
            isDecreasing = true;
            Debug.Log(audioSource.volume);
        }
        else
        {
            if (1.1f - (offset / 20) <= 1)
                audioSource.volume = 1.1f - (offset / 20);
            else
                audioSource.volume = 1;
            isDecreasing = false;
        }
        */

        offset = Mathf.Abs(camera.transform.position.y - transform.position.y);
        if (offset>20 && audioSource.volume != 0)
        {
            audioSource.volume = 0;
        }
        if (offset < 20)
        {
            if (offset < 10)
            {
                audioSource.volume = 1 - (offset / 30);
            }
            else
            {
                audioSource.volume = 1 - (offset / 20);
            }
            
        }
    }
}
