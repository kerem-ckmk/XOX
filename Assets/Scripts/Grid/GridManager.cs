using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int GridSize { get; private set; }
    public bool IsInitialized { get; private set; }

    public void Initialize(int defaultGridSize)
    {
        GridSize = defaultGridSize;

        IsInitialized = true;
    }
    public void Rebuild(int gridSize)
    {
        GridSize = gridSize;
        Debug.Log("GridSize:" + GridSize);
    }
}
