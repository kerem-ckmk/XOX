using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("References")]
    public int maxGridSize = 8;
    public int MatchScore { get; private set; }
    public bool IsInitialized { get; private set; }
    public event Action<int> OnMatchScoreUpdate;

    public void Initialize()
    {
        IsInitialized = true;
    }

    public void GridRebuild()
    {

    }
}
