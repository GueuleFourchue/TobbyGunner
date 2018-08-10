using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour {

    ChunksManager chunksManager;

    private void Awake()
    {
        chunksManager = GameObject.Find("GameManager").GetComponent<ChunksManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chunksManager.AddChunk();
            Destroy(this.gameObject);
        }
    }
}
