using UnityEngine;

public class XController : MonoBehaviour
{
    public int ID { get; private set; } = -1;
    public bool IsInitialized { get; private set; }

    public void Initialize(int id, Transform xTransform)
    {
        ID = id;
        transform.SetParent(xTransform);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        IsInitialized = true;
    }
}
