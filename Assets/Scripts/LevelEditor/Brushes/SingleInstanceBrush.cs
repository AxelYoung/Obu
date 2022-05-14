using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SingleInstanceBrush : BaseBrush {
    Vector3Int instance;

    void Start() {
        CanPlaceTile += CreateSingleInstance;
    }

    public bool CreateSingleInstance() {
        if (brushManager == null) return true;
        if (brushManager.tilemap.GetTile(instance) == tile) return false;
        else {
            instance = tilePosition;
            return true;
        }
    }
}
