using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public int defaultGridSize = 3;
    public int maxGridSize = 8;
    [Header("Sound")]
    public AudioClip buttonClip;

    public int MatchScore { get; private set; }
    public bool IsInitialized { get; private set; }
    public AudioSource AudioSource { get; private set; }

    public event Action<int> OnMatchScoreUpdate;

    public void Initialize()
    {
        SetAudioSource();
        IsInitialized = true;
    }

    public void GridRebuild()
    {

    }

    private void SetAudioSource()
    {
        AudioSource = this.gameObject.AddComponent<AudioSource>();
        AudioSource.playOnAwake = false;
        AudioSource.clip = buttonClip;
        AudioSource.volume = 0.4f;
    }

    public void PlaySound()
    {
        AudioSource.Play();
    }
}
