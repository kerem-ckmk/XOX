using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XManager : MonoBehaviour
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;
    }
}
