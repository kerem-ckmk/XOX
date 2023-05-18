using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public int maxGridSize = 8;
    [Header("Sound")]
    public AudioClip buttonClip;

    public int MatchScore { get; private set; }
    public bool IsInitialized { get; private set; }
    public AudioSource AudioSource { get; private set; }

    public event Action<int> OnMatchScoreUpdate;

    public void Initialize()
    {
        AudioSource = this.gameObject.AddComponent<AudioSource>();
        AudioSource.playOnAwake = false;
        AudioSource.clip = buttonClip;

        IsInitialized = true;
    }

    public void GridRebuild()
    {

    }

    public void PlaySound()
    {
        AudioSource.Play();
    }
}
