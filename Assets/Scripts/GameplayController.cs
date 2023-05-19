using System;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public int defaultGridSize = 3;
    public int maxGridSize = 8;
    [Header("Sound")]
    public AudioClip buttonClip;
    [Header("Managers")]
    public GridController gridController;
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
        gridController.Initialize(defaultGridSize);
        gridController.OnCreateItem += GridController_OnCreateItem;
        gridController.OnDestroyItem += GridController_OnDestroyItem;
        x_Manager.Initialize();
        IsInitialized = true;
    }

    private void GridController_OnDestroyItem(Vector2 cellInfo, Transform cellTransform)
    {
        x_Manager.DestroyXController(cellInfo,cellTransform);
    }

    private void GridController_OnCreateItem(Vector2 cellInfo, Transform cellTransform)
    {
        x_Manager.SpawnXController(cellInfo,cellTransform);
    }

    public void GridRebuild(string gridSizeStr)
    {
        int gridSize = string.IsNullOrEmpty(gridSizeStr) ? defaultGridSize : int.Parse(gridSizeStr);
        gridSize = Mathf.Clamp(gridSize, _minGridSize, maxGridSize);
        OnChangedInputText?.Invoke(gridSize);
        x_Manager.Rebuild();
        gridController.Rebuild(gridSize);
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
