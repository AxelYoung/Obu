using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[Serializable]
public class LevelData {
    public Bounds bounds = new Bounds();
    public List<TileData> tileData = new List<TileData>();
}


[Serializable]
public class TileData {
    public TileBase tile;
    public Vector3Int position;
    public int tileID;
    public int amount = 0;

    public TileData(TileBase tileBase, Vector3Int position, int tileID) {
        this.tile = tileBase;
        this.position = position;
        this.tileID = tileID;
    }

    public TileData(TileBase tileBase, Vector3Int position, int tileID, int amount) {
        this.tile = tileBase;
        this.position = position;
        this.tileID = tileID;
        this.amount = amount;
    }
}

[Serializable]
public struct TileGameObject {
    public TileBase tile;
    public GameObject prefab;
}