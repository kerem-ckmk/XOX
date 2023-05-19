using System;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("References")]
    public float maxWidth;
    public float cellSpacing;

    public CellController cellControllerPrefab;
    public List<CellController> CellControllers { get; private set; }
    public int GridSize { get; private set; }
    public bool IsInitialized { get; private set; }

    public event Action<Vector2, Transform> OnCreateItem;
    public event Action<Vector2, Transform> OnDestroyItem;
    private float _cellSize;
    public void Initialize(int defaultGridSize)
    {
        CellControllers = new List<CellController>();
        CellControllers.Clear();
        GridSize = defaultGridSize;
        Rebuild(GridSize);
        IsInitialized = true;
    }
    public void Rebuild(int gridSize)
    {
        GridSize = gridSize;
        int cellCount = GridSize * GridSize;

        foreach (var cell in CellControllers)
            cell.gameObject.SetActive(false);
       
        for (int i = 0; i < cellCount; i++)
            SpawnCellController(i);
    }

    public CellController SpawnCellController(int cellIndex)
    {
        CellController cellControllerObject = null;

        foreach (var cellController in CellControllers)
            if (!cellController.gameObject.activeSelf)
                cellControllerObject = cellController;

        if (cellControllerObject == null)
            cellControllerObject = CreateCellController();

        Vector2 cellInfo = CalculateGrid(cellIndex);
        cellControllerObject.transform.localPosition = CalculatePosition(cellInfo);
        cellControllerObject.transform.localScale = _cellSize * Vector3.one;
        cellControllerObject.Initialize(cellInfo);
        cellControllerObject.OnCreateItem += CellControllerObject_OnCreateItem;
        cellControllerObject.OnDestroyItem += CellControllerObject_OnDestroyItem;
        cellControllerObject.gameObject.SetActive(true);

        return cellControllerObject;
    }

    private void CellControllerObject_OnDestroyItem(Vector2 cellInfo, Transform cellTransform)
    {
        OnDestroyItem?.Invoke(cellInfo, cellTransform);
    }

    private void CellControllerObject_OnCreateItem(Vector2 cellInfo, Transform cellTransform)
    {
        OnCreateItem?.Invoke(cellInfo, cellTransform);
    }

    public CellController CreateCellController()
    {
        var cellControllerObject = Instantiate(cellControllerPrefab, transform);
        CellControllers.Add(cellControllerObject);
        return cellControllerObject;
    }

    private Vector3 CalculatePosition(Vector2 gridInfo)
    {
        Vector3 cellPosition;
        _cellSize = (maxWidth - (cellSpacing * (GridSize - 1))) / GridSize;
        Vector3 startPos = transform.localPosition - new Vector3((_cellSize + cellSpacing) * (GridSize - 1) * 0.5f, (_cellSize + cellSpacing) * (GridSize - 1) * 0.5f, 0f);

        int x = (int)gridInfo.x;
        int y = (int)gridInfo.y;
        cellPosition = startPos + new Vector3(x * (_cellSize + cellSpacing), y * (_cellSize + cellSpacing), 0f);

        return cellPosition;
    }

    private Vector2 CalculateGrid(int cellIndex)
    {
        int x = cellIndex % GridSize;
        int y = cellIndex / GridSize;
        return new Vector2(x, y);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < GridSize * GridSize; i++)
        {
            Vector2 gridInfo = CalculateGrid(i);
            Vector3 localPosition = transform.TransformPoint(CalculatePosition(gridInfo));
            Gizmos.DrawCube(localPosition, _cellSize * Vector3.one);
        }
    }
#endif
}


