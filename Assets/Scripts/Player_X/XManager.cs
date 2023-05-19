using System;
using System.Collections.Generic;
using UnityEngine;

public class XManager : MonoBehaviour
{
    public XController xControllerPrefab;
    public List<XController> XControllers { get; private set; }
    public List<XController> NeighborGroupList { get; private set; }
    public bool IsInitialized { get; private set; }
    public event Action MissionCompleted;
    public event Action<Vector2> OnClosedX;


    public void Initialize()
    {
        XControllers = new List<XController>();
        XControllers.Clear();
        NeighborGroupList = new List<XController>();
        NeighborGroupList.Clear();
        IsInitialized = true;
    }

    public XController SpawnXController(Vector2 xInfo, Transform xTransform)
    {
        XController xControllerObject = null;

        foreach (var xController in XControllers)
        {
            if (!xController.gameObject.activeSelf)
            {
                xControllerObject = xController;
                break;
            }
        }

        if (xControllerObject == null)
        {
            xControllerObject = CreateXController();
        }

        xControllerObject.Initialize(xTransform, xInfo);
        xControllerObject.OnClose += XControllerObject_OnClose;
        xControllerObject.gameObject.SetActive(true);
        UpdateNeighbors();
        return xControllerObject;
    }

    private void XControllerObject_OnClose(XController closeXController)
    {
        OnClosedX?.Invoke(closeXController.Info);
    }

    public void Rebuild()
    {
        foreach (var xController in XControllers)
        {
            xController.gameObject.SetActive(false);
            xController.OnClose -= XControllerObject_OnClose;
        }
    }

    public void DestroyXController(Vector2 xInfo, Transform xTransform)
    {
        foreach (var xController in XControllers)
        {
            if (xController.gameObject.activeSelf && xController.Info == xInfo)
            {
                xController.gameObject.SetActive(false);
                xController.OnClose -= XControllerObject_OnClose;
                break;
            }
        }

        UpdateNeighbors();
    }

    public XController CreateXController()
    {
        var xControllerObject = Instantiate(xControllerPrefab, transform);
        XControllers.Add(xControllerObject);
        return xControllerObject;
    }

    public void UpdateNeighbors()
    {
        foreach (var xController in XControllers)
        {
            if (!xController.gameObject.activeSelf)
            {
                xController.ClearNeighbors();
                continue;
            }

            xController.ClearNeighbors();

            foreach (var otherController in XControllers)
            {
                if (xController == otherController || !otherController.gameObject.activeSelf)
                    continue;

                bool isHorizontalNeighbor = Mathf.Abs(xController.Info.x - otherController.Info.x) == 1 &&
                                            Mathf.Approximately(xController.Info.y, otherController.Info.y);
                bool isVerticalNeighbor = Mathf.Approximately(xController.Info.x, otherController.Info.x) &&
                                          Mathf.Abs(xController.Info.y - otherController.Info.y) == 1;

                if (isHorizontalNeighbor || isVerticalNeighbor)
                {
                    xController.AddNeighbor(otherController);
                }
            }
        }

        foreach (var xController in XControllers)
            if (xController.Neighbors.Count >= 2)
            {
                MissionCompleted?.Invoke();
                xController.CloseNeighborsRecursive();
                break;
            }
        
    }
}
