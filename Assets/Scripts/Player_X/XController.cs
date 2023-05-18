using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XController : MonoBehaviour
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;
    }
}
