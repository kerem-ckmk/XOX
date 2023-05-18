using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("References")]
    public float maxWidth;
    public int gridSize = 4;
    public bool IsInitialized { get; private set; }

    private float _cellSpacing = 0.05f;
    private float _cellSize;
    public void Initialize(int defaultGridSize)
    {
        //GridSize = defaultGridSize;

        IsInitialized = true;
    }
    public void Rebuild(int gridSize)
    {
        // GridSize = gridSize;
        //Debug.Log("GridSize:" + GridSize);
    }


    private Vector3 CalculatePosition(Vector2 gridInfo)
    {
        Vector3 cellPosition = Vector3.zero;
        _cellSize = (maxWidth - (_cellSpacing * (gridSize - 1))) / gridSize;
        Vector3 startPos = transform.position - new Vector3((_cellSize + _cellSpacing) * (gridSize - 1) * 0.5f, (_cellSize + _cellSpacing) * (gridSize - 1) * 0.5f, 0f);

        int x = (int)gridInfo.x;
        int y = (int)gridInfo.y;
        cellPosition = startPos + new Vector3(x * (_cellSize + _cellSpacing), y * (_cellSize + _cellSpacing), 0f);

        return cellPosition;
    }

    private Vector2 CalculateGrid(int gridIndex)
    {
        int x = gridIndex % gridSize;
        int y = gridIndex / gridSize;
        return new Vector2(x, y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < gridSize * gridSize; i++)
        {
            Vector2 gridInfo = CalculateGrid(i);
            Gizmos.DrawCube(CalculatePosition(gridInfo), _cellSize * Vector2.one);
        }
    }

}


