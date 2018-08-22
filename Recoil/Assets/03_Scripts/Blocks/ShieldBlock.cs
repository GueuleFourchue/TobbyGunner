using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    public GameObject deathParticlePrefab;

    public void Destroy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject particle = GameObject.Instantiate(deathParticlePrefab);
        particle.transform.position = transform.position;

        Destroy(particle, 1f);
        Destroy(this.gameObject);
    }

}
