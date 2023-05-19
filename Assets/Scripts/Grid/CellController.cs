using System;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class CellController : MonoBehaviour
{
    public CellVisual cellVisual;
    public bool IsInitialized { get; private set; }
    public Vector2 CellInfo { get; private set; }
    public bool OnHaveItem { get; private set; }

    public event Action<Vector2,Transform> OnCreateItem;
    public event Action<Vector2, Transform> OnDestroyItem;

    public void Initialize(Vector2 cellInfo)
    {
        OnHaveItem = false;
        CellInfo = cellInfo;
        cellVisual.OnClickCell += CellVisual_OnClickCell;
        IsInitialized = true;
    }
    private void CellVisual_OnClickCell()
    {
        if (OnHaveItem)
        {
            OnDestroyItem?.Invoke(CellInfo,transform);
            OnHaveItem = false;
            return;
        }

        OnHaveItem = true;
        OnCreateItem?.Invoke(CellInfo,transform);
    }

    private void OnDestroy()
    {
        OnCreateItem = null;
        OnDestroyItem = null;
    }
}
