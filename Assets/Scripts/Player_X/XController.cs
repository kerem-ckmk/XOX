using UnityEngine;

public class XController : MonoBehaviour
{
    public int ID { get; private set; } = -1;
    public Vector2 Info { get; private set; }
    public bool IsInitialized { get; private set; }

    public void Initialize(int id, Transform xTransform, Vector2 xInfo)
    {
        ID = id;
        Info = xInfo;
        transform.SetParent(xTransform);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector3.one;
        IsInitialized = true;
    }
}
