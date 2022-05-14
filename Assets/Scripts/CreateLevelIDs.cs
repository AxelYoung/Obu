using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class CreateLevelIDs : MonoBehaviour {
    public TileBase[] allTiles;

    void Awake() {
        LevelIDs.tileBases = allTiles;
    }
}

[Serializable]
public static class LevelIDs {
    public static string levelName;
    public static TileBase[] tileBases;

    public static int GetTileID(TileBase tileBase) {
        for (int i = 0; i < tileBases.Length; i++) {
            if (tileBases[i] == tileBase) {
                return i;
            }
        }
        return 100;
    }
}