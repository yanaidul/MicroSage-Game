// Field.cs
using System.Collections.Generic;
using UnityEngine;

public class FieldSesat : MonoBehaviour
{
    [SerializeField]
    private int _stageSolvedTarget;

    private TileSesat[,] _grid;
    private bool _canDrawConnection;

    private List<TileSesat> _currentPath = new List<TileSesat>();
    private TileSesat _currentTile;
    private int _currentPathId = 0;

    private int _dimensionX;
    private int _dimensionY;

    private void Start()
    {
        _dimensionX = transform.childCount;
        _dimensionY = transform.GetChild(0).transform.childCount;
        _grid = new TileSesat[_dimensionX, _dimensionY];

        for (int y = 0; y < _dimensionX; y++)
        {
            var row = transform.GetChild(y).transform;
            for (int x = 0; x < _dimensionY; x++)
            {
                TileSesat tile = row.GetChild(x).GetComponent<TileSesat>();
                _grid[x, y] = tile;
            }
        }
    }

    private void Update()
    {
        if (_canDrawConnection)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int gridX = Mathf.FloorToInt(mousePosition.x);
            int gridY = Mathf.FloorToInt(mousePosition.y);

            if (gridX < 0 || gridY < 0 || gridX >= _dimensionX || gridY >= _dimensionY)
                return;

            TileSesat hoverTile = _grid[gridX, gridY];

            if (hoverTile != null && !_currentPath.Contains(hoverTile))
            {
                hoverTile.AddConnectionLayer(
                    _currentPathId,
                    _currentTile.GetComponent<SpriteRenderer>().color
                );
                _currentPath.Add(hoverTile);
                _currentTile = hoverTile;
            }
        }
    }

    public void StartConnection(TileSesat startTile, int pathId, Color color)
    {
        _canDrawConnection = true;
        _currentTile = startTile;
        _currentPathId = pathId;
        _currentPath.Clear();
        startTile.AddConnectionLayer(pathId, color);
        _currentPath.Add(startTile);
    }

    public void EndConnection()
    {
        _canDrawConnection = false;
        _currentTile = null;
        _currentPathId = 0;
        _currentPath.Clear();
    }

    public void ResetAllConnections()
    {
        foreach (TileSesat tile in _grid)
        {
            tile.ResetConnections();
        }
    }
}
