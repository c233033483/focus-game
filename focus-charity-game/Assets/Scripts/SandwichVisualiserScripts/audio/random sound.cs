using System.Collections.Generic;
using UnityEngine;

public class randomsound : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public List<AudioClip> randomAudioClips;

    public AudioSource audioSource;

    
    public void SelectedAudio(int clipID)//plays a selected audio clip from Audio Clips list
    {
        AudioClip clip = audioClips[clipID];
        audioSource.clip = clip;
        audioSource.Play();
    }
    
    public void RandomAudio()//plays random audio clip from Random Audio Clips list
    {
        int r = Random.Range(0, randomAudioClips.Count);
        AudioClip clip = randomAudioClips[r];
        audioSource.clip = clip;
        audioSource.Play();
    }


}
