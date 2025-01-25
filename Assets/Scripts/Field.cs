using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private int _stageSolvedTarget;

    [SerializeField]
    private Led _led;

    private Tile[,] _grid;
    private bool _canDrawConnection = false;

    private List<Tile> _latestConnections = new List<Tile>();
    private Tile _connectionTile;

    private List<int> _solvedConnections = new List<int>();

    private int _dimensionX = 0;
    private int _dimensionY = 0;
    private int _solved = 0;
    private Dictionary<int, int> _amountToSolve = new Dictionary<int, int>();

    void Start()
    {
        _dimensionX = transform.childCount;
        _dimensionY = transform.GetChild(0).transform.childCount;
        _grid = new Tile[_dimensionX, _dimensionY + 1];
        for (int y = 0; y < _dimensionX; y++)
        {
            var row = transform.GetChild(y).transform;
            row.gameObject.name = "" + y;
            for (int x = 0; x < _dimensionY; x++)
            {
                var tile = row.GetChild(x).GetComponent<Tile>();
                tile.gameObject.name = "" + x;
                tile.onSelected.AddListener(OnTileSelected);
                CollectAmountToSolveFromTile(tile);
                _grid[x, y] = tile;
            }
        }
        SetGameStatus(_solved, _amountToSolve.Count);
        OutputGrid();
    }

    void CollectAmountToSolveFromTile(Tile tile)
    {
        if (tile.cid > Tile.UNPLAYABLE_INDEX)
        {
            if (_amountToSolve.ContainsKey(tile.cid))
                _amountToSolve[tile.cid] += 1;
            else
                _amountToSolve[tile.cid] = 1;
        }
    }

    void OutputGrid()
    {
        var results = "";
        int dimension = transform.childCount;
        for (int y = 0; y < dimension; y++)
        {
            results += "{";
            var row = transform.GetChild(y).transform;
            for (int x = 0; x < row.childCount; x++)
            {
                var tile = _grid[x, y];
                if (x > 0)
                    results += ",";
                results += tile.cid;
            }
            results += "}\n";
        }
        Debug.Log("Main -> Start: _grid: \n" + results);
    }

    Vector3 _mouseWorldPosition;
    int _mouseGridX,
        _mouseGridY;

    void Update()
    {
        if (_canDrawConnection)
        {
            _mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouseGridX = (int)Mathf.Floor(_mouseWorldPosition.x);
            _mouseGridY = (int)Mathf.Floor(_mouseWorldPosition.y);

            if (CheckMouseOutsideGrid())
                return;

            Tile hoverTile = _grid[_mouseGridX, _mouseGridY];
            Tile firstTile = _latestConnections[0];
            if (_mouseGridX >= 10 || _mouseGridY >= 12)
                return;
            bool isDifferentActiveTile = hoverTile.cid > 0 && hoverTile.cid != firstTile.cid;

            if (
                hoverTile.isHighlighted
                || hoverTile.isSolved
                || (isDifferentActiveTile && !hoverTile.IsFromGenerator)
                || hoverTile.IsNotGrid
            )
                return;

            Vector2 connectionTilePosition = FindTileCoordinates(_connectionTile);
            bool isPositionDifferent = IsDifferentPosition(
                _mouseGridX,
                _mouseGridY,
                connectionTilePosition
            );

            Debug.Log(
                "Field -> OnMouseDrag("
                    + isPositionDifferent
                    + "): "
                    + _mouseGridX
                    + "|"
                    + _mouseGridY
            );

            if (isPositionDifferent)
            {
                var deltaX = System.Math.Abs(connectionTilePosition.x - _mouseGridX);
                var deltaY = System.Math.Abs(connectionTilePosition.y - _mouseGridY);
                bool isShiftNotOnNext = deltaX > 1 || deltaY > 1;
                bool isShiftDiagonal = (deltaX > 0 && deltaY > 0);
                Debug.Log(
                    "Field -> OnMouseDrag: isShiftNotOnNext = "
                        + isShiftNotOnNext
                        + "| isShiftDiagonal = "
                        + isShiftDiagonal
                );
                if (isShiftNotOnNext || isShiftDiagonal)
                    return;

                hoverTile.Highlight();
                hoverTile.SetConnectionColor(_connectionTile.ConnectionColor);
                _connectionTile.ConnectionToSide(
                    _mouseGridY > connectionTilePosition.y,
                    _mouseGridX > connectionTilePosition.x,
                    _mouseGridY < connectionTilePosition.y,
                    _mouseGridX < connectionTilePosition.x
                );

                _connectionTile = hoverTile;
                _latestConnections.Add(_connectionTile);

                if (CheckIfTilesMatch(hoverTile, firstTile))
                {
                    _latestConnections.ForEach((tile) => tile.isSolved = true);
                    _canDrawConnection = false;
                    _amountToSolve.Remove(firstTile.cid);
                    SetGameStatus(++_solved, _amountToSolve.Count + _solved);
                    if (_amountToSolve.Keys.Count == 0)
                    {
                        Debug.Log("GAME COMPLETE");
                    }
                }
            }
        }
    }

    bool CheckIfTilesMatch(Tile tile, Tile another)
    {
        return tile.cid > 0 && another.cid == tile.cid && !tile.IsFromGenerator;
    }

    bool CheckMouseOutsideGrid()
    {
        return _mouseGridY >= _dimensionY + 1
            || _mouseGridY < 0
            || _mouseGridX >= _dimensionX
            || _mouseGridX < 0;
    }

    void OnTileSelected(Tile tile)
    {
        Debug.Log("Field -> onTileSelected(" + tile.isSelected + "): " + FindTileCoordinates(tile));
        if (tile.isSelected)
        {
            _connectionTile = tile;
            _latestConnections = new List<Tile>();
            _latestConnections.Add(_connectionTile);
            _canDrawConnection = true;
            _connectionTile.Highlight();
        }
        else
        {
            bool isFirstTileInConnection = _connectionTile == tile;
            if (isFirstTileInConnection)
                tile.HightlightReset();
            else if (!CheckIfTilesMatch(_connectionTile, tile))
            {
                ResetConnections();
            }
            _canDrawConnection = false;
        }
    }

    public void OnRestart()
    {
        Debug.Log("Field -> onRestart");
        int dimension = transform.childCount;
        for (int y = 0; y < dimension; y++)
        {
            var row = transform.GetChild(y).transform;
            for (int x = 0; x < row.childCount; x++)
            {
                var tile = _grid[x, y];
                tile.ResetConnection();
                tile.HightlightReset();
                CollectAmountToSolveFromTile(tile);
            }
        }
        _solved = 0;
        _led.OnTurnOffLED();
        SetGameStatus(_solved, _amountToSolve.Count);
    }

    public void OnUndo()
    {
        Debug.Log("Field -> onUndo");
        foreach (Tile connection in _latestConnections)
        {
            connection.ResetConnection();
            connection.HightlightReset();
        }
        if (_solved > 0)
            _solved--;
        _led.OnTurnOffLED();
        SetGameStatus(_solved, _amountToSolve.Count);
    }

    void SetGameStatus(int solved, int from)
    {
        //GameObject.Find("txtStatus").GetComponent<UnityEngine.UI.Text>().text = "Solve: " + solved + " from " + from;
    }

    void ResetConnections()
    {
        Debug.Log("Field -> _ResetConnections: _connections.Count = " + _latestConnections.Count);
        _latestConnections.ForEach(
            (tile) =>
            {
                tile.ResetConnection();
                tile.HightlightReset();
            }
        );
    }

    public bool CheckCurrentSolvedStatus()
    {
        if (_solved == _stageSolvedTarget)
            return true;
        else
            return false;
    }

    Vector2 FindTileCoordinates(Tile tile)
    {
        int x = int.Parse(tile.gameObject.name);
        int y = int.Parse(tile.gameObject.transform.parent.gameObject.name);
        return new Vector2(x, y);
    }

    public bool IsDifferentPosition(int gridX, int gridY, Vector2 position)
    {
        return position.x != gridX || position.y != gridY;
    }

    private class Connection
    {
        public Tile tile;
        public Vector2 position;

        public Connection(Tile tile, Vector2 position)
        {
            this.tile = tile;
            this.position = position;
        }

        public bool IsDifferentPosition(int gridX, int gridY)
        {
            return this.position.x != gridX || this.position.y != gridY;
        }
    }
}
