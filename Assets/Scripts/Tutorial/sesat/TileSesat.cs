// Tile.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileSesat : MonoBehaviour
{
    public static int UNPLAYABLE_INDEX = 0;
    public static Color COLOR_HIGHLIGHT = new Color(1, 1, 0, 0.05f);
    public static string NAME_CONNECTION = "Connection";
    public static string NAME_BACK = "Back";

    public int cid = 0;
    private Dictionary<int, GameObject> connectionLayers = new Dictionary<int, GameObject>();

    public bool isSolved;
    public bool isPlayable;

    private SpriteRenderer BackComponentRenderer =>
        this.transform.Find(NAME_BACK).GetComponent<SpriteRenderer>();

    private void Start()
    {
        isPlayable = cid > UNPLAYABLE_INDEX;
        if (!isPlayable)
            Destroy(this.gameObject);
    }

    public void AddConnectionLayer(int layerId, Color color)
    {
        if (connectionLayers.ContainsKey(layerId))
            return;

        GameObject newLayer = new GameObject($"ConnectionLayer_{layerId}");
        newLayer.transform.SetParent(this.transform.Find(NAME_CONNECTION));
        var renderer = newLayer.AddComponent<SpriteRenderer>();
        renderer.color = color;
        renderer.sortingOrder = layerId;

        connectionLayers[layerId] = newLayer;
    }

    public void ResetConnections()
    {
        foreach (var layer in connectionLayers.Values)
        {
            Destroy(layer);
        }
        connectionLayers.Clear();
    }
}
