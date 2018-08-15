using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueDeactivate : MonoBehaviour {

    float originSpeed;
    MonsterBehaviour monsterBehaviour;
    public DeathZone deathZone;

    private void Start()
    {
        if (transform.GetComponent<MonsterBehaviour>() != null)
            monsterBehaviour = transform.GetComponent<MonsterBehaviour>();
    }

    public void TimerStandby()
    {
        if (monsterBehaviour != null)
        {
            originSpeed = monsterBehaviour.horizontalMoveSpeed;
            monsterBehaviour.horizontalMoveSpeed = 0;
            transform.GetComponentInChildren<Animator>().enabled = false;
        } 
        if (deathZone != null)
        {
            originSpeed = deathZone.moveSpeed;
            deathZone.moveSpeed = 0;
        }
    }

    public void EndTimerStandby()
    {
        if (monsterBehaviour != null)
        {
            monsterBehaviour.horizontalMoveSpeed = originSpeed;
            if (transform.GetComponentInChildren<Animator>() != null)
                transform.GetComponentInChildren<Animator>().enabled = true;
        }
        if (deathZone != null)
        {
            deathZone.moveSpeed = originSpeed;
        }
    }
}
