using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip menuSong, combatSong;

    [Space(10)]
    public SFXClips sfxClips;


    public void PlaySong(AudioClip songClip)
    {
        if(musicSource.clip != songClip)
        {
            musicSource.Stop();
            musicSource.clip = songClip;
            musicSource.Play();
        }
    }
    public void PlaySFX(AudioSource sfxSource, AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        sfxSource.Play();
    }
}
[System.Serializable]
public class SFXClips
{
    public AudioClip walkSFX;
    public AudioClip attackSFX;
    public AudioClip blockSFX;
    public AudioClip jumpSFX;
}
