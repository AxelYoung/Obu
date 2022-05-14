using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class BaseBrush : MonoBehaviour {

    public TileBase tile;
    public event Func<bool> CanPlaceTile = () => { return true; };
    public event Action PlacedTile;
    public BrushManager brushManager;
    public Vector3Int tilePosition { get; private set; }

    public void Place() {
        tilePosition = brushManager.tilemap.WorldToCell(transform.position);
        if (CanPlaceTile?.Invoke() == true) {
            CheckForAmountPrompt();
            PlacedTile?.Invoke();
            brushManager.tilemap.SetTile(tilePosition, tile);
        }
    }

    void CheckForAmountPrompt() {
        if (!brushManager.tailModifierUIs.ContainsKey(tilePosition)) return;
        Destroy(brushManager.tailModifierUIs[tilePosition].gameObject);
        brushManager.tailModifierUIs.Remove(tilePosition);
    }
}
