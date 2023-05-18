using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public bool IsInitialized { get; private set; }
    public Vector2 CellInfo { get; private set; }

    public void Initialize(Vector2 cellInfo)
    {
        CellInfo = cellInfo;
        IsInitialized = true;
    }
    
}
