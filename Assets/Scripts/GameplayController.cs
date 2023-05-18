using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public int defaultGridSize = 3;
    public int maxGridSize = 8;
    [Header("Sound")]
    public AudioClip buttonClip;
    [Header("Managers")]
    public GridController gridManager;
    public XManager x_Manager;

    public int MatchScore { get; private set; }
    public bool IsInitialized { get; private set; }
    public AudioSource AudioSource { get; private set; }

    public event Action<int> OnMatchScoreUpdate;
    public event Action<int> OnChangedInputText;

    private int _minGridSize = 3;

    public void Initialize()
    {
        SetAudioSource();
        gridManager.Initialize(defaultGridSize);
        x_Manager.Initialize();
        IsInitialized = true;
    }

    public void GridRebuild(string gridSizeStr)
    {
        int gridSize = string.IsNullOrEmpty(gridSizeStr) ? defaultGridSize : int.Parse(gridSizeStr);
        gridSize = Mathf.Clamp(gridSize, _minGridSize, maxGridSize);
        OnChangedInputText?.Invoke(gridSize);
        gridManager.Rebuild(gridSize);
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
