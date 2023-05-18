using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("References")]
    public float maxWidth;

    public CellController cellControllerPrefab;
    public List<CellController> cellControllers;
    public int GridSize { get; private set; }
    public bool IsInitialized { get; private set; }
    

    private float _cellSpacing = 0f;
    private float _cellSize;
    public void Initialize(int defaultGridSize)
    {
        cellControllers = new List<CellController>();
        cellControllers.Clear();
        GridSize = defaultGridSize;
        Rebuild(GridSize);
        IsInitialized = true;
    }
    public void Rebuild(int gridSize)
    {
        GridSize = gridSize;
        int cellCount = GridSize * GridSize;

        foreach (var cell in cellControllers)
            cell.gameObject.SetActive(false);

        for (int i = 0; i < cellCount; i++)
            SpawnCellController(i);
    }

    public CellController SpawnCellController(int cellIndex)
    {
        CellController cellControllerObject = null;

        foreach (var cellController in cellControllers)
            if (!cellController.gameObject.activeSelf)
                cellControllerObject = cellController;

        if (cellControllerObject == null)
            cellControllerObject = CreateCellController();

        Vector2 cellInfo = CalculateGrid(cellIndex);
        cellControllerObject.transform.localPosition = CalculatePosition(cellInfo);
        cellControllerObject.transform.localScale = _cellSize * Vector3.one;
        cellControllerObject.Initialize(cellInfo);
        cellControllerObject.gameObject.SetActive(true);

        return cellControllerObject;
    }

    public CellController CreateCellController()
    {
        var cellControllerObject = Instantiate(cellControllerPrefab, transform);
        cellControllers.Add(cellControllerObject);
        return cellControllerObject;
    }


    private Vector3 CalculatePosition(Vector2 gridInfo)
    {
        Vector3 cellPosition;
        _cellSize = (maxWidth - (_cellSpacing * (GridSize - 1))) / GridSize;
        Vector3 startPos = transform.localPosition - new Vector3((_cellSize + _cellSpacing) * (GridSize - 1) * 0.5f, (_cellSize + _cellSpacing) * (GridSize - 1) * 0.5f, 0f);

        int x = (int)gridInfo.x;
        int y = (int)gridInfo.y;
        cellPosition = startPos + new Vector3(x * (_cellSize + _cellSpacing), y * (_cellSize + _cellSpacing), 0f);

        return cellPosition;
    }

    private Vector2 CalculateGrid(int cellIndex)
    {
        int x = cellIndex % GridSize;
        int y = cellIndex / GridSize;
        return new Vector2(x, y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < GridSize * GridSize; i++)
        {
            Vector2 gridInfo = CalculateGrid(i);
            Gizmos.DrawCube(CalculatePosition(gridInfo), _cellSize * Vector2.one);
        }
    }

}


