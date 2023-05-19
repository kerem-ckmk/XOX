using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XManager : MonoBehaviour
{
    public XController xControllerPrefab;
    public List<XController> xControllers;
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        xControllers = new List<XController>();
        xControllers.Clear();
        IsInitialized = true;
    }

    public XController SpawnXController()
    {
        XController xControllerObject = null;

        foreach (var xController in xControllers)
        {
            if (!xController.gameObject.activeSelf)
            {
                xControllerObject = xController;
            }
        }

        if (xControllerObject == null)
        {
            xControllerObject = CreateXController(); 
        }

        xControllerObject.Initialize();

        return xControllerObject;
    }
    public XController CreateXController()
    {
        var xControllerObject = Instantiate(xControllerPrefab,transform);
        xControllers.Add(xControllerObject);
        return xControllerObject;
    }
}
