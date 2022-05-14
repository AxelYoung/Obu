using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleBrush : BaseBrush {
    public TileBase offTile;
    public TileBase onTile;
    public Sprite offSprite;
    public Sprite onSprite;
    public ToggleBrush partnerToggle;
    public bool state = false;
    SpriteRenderer spriteRenderer;
    List<Vector3Int> allInstances = new List<Vector3Int>();

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = state ? onSprite : offSprite;
        tile = state ? onTile : offTile;

        PlacedTile += () => { allInstances.Add(tilePosition); };
        CanPlaceTile += ToggleState;
    }

    bool ToggleState() {
        if (partnerToggle.state == state && brushManager.tilemap.GetTile(tilePosition) != tile) return true;

        state = !state;
        foreach (Vector3Int instance in allInstances) {
            if (brushManager.tilemap.GetTile(instance) == tile) {
                brushManager.tilemap.SetTile(instance, state ? onTile : offTile);
            }
        }
        tile = state ? onTile : offTile;
        spriteRenderer.sprite = state ? onSprite : offSprite;
        if (partnerToggle.spriteRenderer == null) partnerToggle.state = state;
        if (partnerToggle.state != state) partnerToggle.ToggleState();
        return false;
    }
}
