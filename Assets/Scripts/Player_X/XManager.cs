using System.Collections.Generic;
using UnityEngine;

public class XManager : MonoBehaviour
{
    public XController xControllerPrefab;
    public List<XController> xControllers;
    public bool IsInitialized { get; private set; }
    private int _xID;
    public void Initialize()
    {
        xControllers = new List<XController>();
        xControllers.Clear();
        IsInitialized = true;
    }

    public XController SpawnXController(Vector2 xInfo, Transform xTransform)
    {
        XController xControllerObject = null;

        foreach (var xController in xControllers)
            if (!xController.gameObject.activeSelf)
                xControllerObject = xController;

        if (xControllerObject == null)
            xControllerObject = CreateXController();

        _xID += 1;
        xControllerObject.Initialize(_xID, xTransform, xInfo);
        xControllerObject.gameObject.SetActive(true);
        return xControllerObject;
    }

    public void Rebuild()
    {
        foreach (var xController in xControllers)
            xController.gameObject.SetActive(false);
    }

    public void DestroyXController(Vector2 xInfo, Transform xTransform)
    {
        foreach (var xController in xControllers)
            if (xController.gameObject.activeSelf && xController.Info == xInfo)
                xController.gameObject.SetActive(false);
    }
    public XController CreateXController()
    {
        var xControllerObject = Instantiate(xControllerPrefab, transform);
        xControllers.Add(xControllerObject);
        return xControllerObject;
    }
}
