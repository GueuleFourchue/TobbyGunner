using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterEye : MonoBehaviour {

    public Transform player;
    public float leftClamp;
    public float rightClamp;

    
    void Update ()
    {
        if (player.position.x < rightClamp && player.position.x > leftClamp)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        else if (player.position.x < leftClamp)
        {
            transform.position = new Vector3(leftClamp, transform.position.y, transform.position.z);
        }
        else if (player.position.x > rightClamp)
        {
            transform.position = new Vector3(rightClamp, transform.position.y, transform.position.z);
        }
    }
}
