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
        offset = camera.transform.position.y - transform.position.y;
        if (Mathf.Abs(offset) > 20 && audioSource.volume!=0 && !isDecreasing)
        {
            isDecreasing = true;
            audioSource.DOKill();
            audioSource.DOFade(0, 2);
        }
        else
        {
            if (1.1f - (offset / 20) <= 1)
                audioSource.volume = 1.1f - (offset / 20);
            else
                audioSource.volume = 1;
        }
    }
}
