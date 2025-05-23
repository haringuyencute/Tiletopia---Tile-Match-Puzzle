using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------- Audio Source --------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------------- Audio Clip -----------------")]
    public AudioClip background;
    public AudioClip tileCLick;
    public AudioClip moveTile;

    public VolumeSetting volumeSetting;

    public void Init()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clipParam)
    {
        SFXSource.PlayOneShot(clipParam);
    }
}
