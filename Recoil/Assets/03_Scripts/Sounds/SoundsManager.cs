using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Represents an inventory slot containing some count of some item.
    [System.Serializable] // This makes the struct visible in the Inspector.
    public struct Slot
    {
        public string name;
        public AudioSource audioSource;
        [Range(0f,1f)]
        public float volume;
        public bool randomPitch;
    }

    // Exposes a list of slots in the Inspector.
    public Slot[] slots;

    public void PlaySound(string name)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].name == name)
            {
                slots[i].audioSource.Stop();
                if (slots[i].randomPitch == true)
                {
                    slots[i].audioSource.pitch = Random.Range(0.95f, 1.05f);
                }
                slots[i].audioSource.volume = slots[i].volume;
                slots[i].audioSource.Play();
            }
        }
    }
}
