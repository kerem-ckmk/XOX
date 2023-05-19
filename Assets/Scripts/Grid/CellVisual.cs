using System;
using UnityEngine;

public class CellVisual : MonoBehaviour
{
    public event Action OnClickCell;

    private void OnMouseDown()
    {
        OnClickCell?.Invoke();
    }

    private void OnDestroy()
    {
        OnClickCell = null;
    }


}
