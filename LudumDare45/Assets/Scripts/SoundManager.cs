using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource[] audiosources;

    public SoundDatabase baseSounds;

    void Start()
    {
        for (int i = 0; i < audiosources.Length; i++)
        {
            audiosources[i].loop = false;
        }
    }

    public void PlaySound(int sound)
    {
        GetAudioSourceAvailable(baseSounds.audioDatabase[sound]);
    }

    public void GetAudioSourceAvailable(AudioClip clip)
    {
        for (int i = 0; i < audiosources.Length; i++)
        {
            if (!audiosources[i].isPlaying)
            {
                audiosources[i].loop = false;
                audiosources[i].clip = clip;
                audiosources[i].Play();
                return;
            }
        }
    }
}
