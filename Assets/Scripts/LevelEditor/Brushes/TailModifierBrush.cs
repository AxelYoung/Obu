using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TailModifierBrush : BaseBrush {

    public GameObject UI;
    public Canvas worldCanvas;

    void Start() {
        PlacedTile += () => { brushManager.tailModifierUIs.Add(tilePosition, Instantiate(UI, transform.position, Quaternion.identity, worldCanvas.transform).GetComponent<TailModifierUI>()); };
        CanPlaceTile += () => { return brushManager.tilemap.GetTile(tilePosition) != tile; };
    }
}