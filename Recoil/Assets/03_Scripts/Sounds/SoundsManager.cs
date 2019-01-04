using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour {

    public AudioMixerGroup SFX_mixer;

    AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        CheckIfAlreadyExists();
    }

    // Represents an inventory slot containing some count of some item.
    [System.Serializable] // This makes the struct visible in the Inspector.
    public struct Slot
    {
        public string name;
        public ClipsHolder clipsHolder;
        [Range(0f, 1f)]
        public float volume;
        public float minPitch;
        public float maxPitch;
    }

    // Exposes a list of slots in the Inspector.
    public Slot[] slots;

    public void PlaySound(string name)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].name == name)
            {
                AudioClip clip = slots[i].clipsHolder.clips[Random.Range(0, slots[i].clipsHolder.clips.Length)];
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.outputAudioMixerGroup = SFX_mixer;
                audioSource.volume = slots[i].volume;
                audioSource.pitch = Random.Range(slots[i].minPitch, slots[i].maxPitch);
                audioSource.Play();
                Destroy(audioSource, clip.length);
            }
        }
    }
    public void StopLastSound()
    {
        audioSource.volume = 0;
    }

    void CheckIfAlreadyExists()
    {
        List<SoundsManager> list = new List<SoundsManager>();
        foreach(SoundsManager manager in GameObject.FindObjectsOfType<SoundsManager>())
        {
            list.Add(manager);
        }
        if(list.Count != 1)
        {
            Destroy(list[1].gameObject);
            list.RemoveAt(1);
        }
    }
}
