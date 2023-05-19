using System;
using System.Collections.Generic;
using UnityEngine;

public class XController : MonoBehaviour
{
    public Vector2 Info { get; private set; }
    public bool IsInitialized { get; private set; }
    public List<XController> Neighbors { get; private set; }

    public event Action<XController> OnClose;

    public void Initialize(Transform xTransform, Vector2 xInfo)
    {
        if (Neighbors == null)
            Neighbors = new List<XController>();

        Neighbors.Clear();

        Info = xInfo;
        transform.position = xTransform.position;
        transform.localScale = xTransform.localScale;
        IsInitialized = true;
    }
    public void AddNeighbor(XController neighborController)
    {
        if (neighborController == null || Neighbors.Contains(neighborController))
            return;

        Neighbors.Add(neighborController);
    }

    public void CloseNeighborsRecursive()
    {
        OnClose?.Invoke(this);
        gameObject.SetActive(false);

        foreach (var neighbor in Neighbors)
        {
            if (neighbor.gameObject.activeSelf)
            {
                neighbor.CloseNeighborsRecursive();
            }
        }

    }

    public void ClearNeighbors()
    {
        Neighbors.Clear();
    }
}
