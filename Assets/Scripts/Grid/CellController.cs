using System;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public CellVisual cellVisual;
    public Vector2 CellInfo { get; private set; }
    public bool OnHaveItem { get; private set; }

    public event Action<CellController> OnCreateItem;
    public event Action<CellController> OnDestroyItem;

    public void Initialize(Vector2 cellInfo)
    {
        CellInfo = cellInfo;
        cellVisual.OnClickCell += CellVisual_OnClickCell;
    }
    private void CellVisual_OnClickCell()
    {
        if (OnHaveItem)
        {
            OnDestroyItem?.Invoke(this);
            OnHaveItem = false;
            return;
        }

        OnHaveItem = true;
        OnCreateItem?.Invoke(this);
    }

    public void Deactive()
    {
        OnHaveItem = false;
        cellVisual.OnClickCell -= CellVisual_OnClickCell;
        gameObject.SetActive(false);
    }

    public void SetHaveItem(bool haveItem)
    {
        OnHaveItem = haveItem;
    }

    private void OnDestroy()
    {
        OnCreateItem = null;
        OnDestroyItem = null;
    }
}
