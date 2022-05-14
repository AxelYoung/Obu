using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class BrushManager : MonoBehaviour {

    public Tilemap tilemap;
    public Bounds gridBounds { get; private set; }

    public GameObject brushRenderer;
    BaseBrush activeBrush;

    Vector3Int mouseGridPosition;
    Vector3Int oldMouseGridPosition;

    public Dictionary<Vector3Int, TailModifierUI> tailModifierUIs = new Dictionary<Vector3Int, TailModifierUI>();

    void Start() {
        SetBrush(brushRenderer);
        int gridSize = GameObject.FindGameObjectWithTag("Grid").GetComponent<CreateGrid>().gridSize;
        gridBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(gridSize, gridSize, 0));
    }

    void Update() {
        Vector2 rawMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (gridBounds.Contains(rawMousePosition)) {
            mouseGridPosition = new Vector3Int(Mathf.FloorToInt(rawMousePosition.x + 0.5f), Mathf.FloorToInt(rawMousePosition.y + 0.5f), 0);
            brushRenderer.transform.position = mouseGridPosition;
            if (!EventSystem.current.IsPointerOverGameObject()) {
                brushRenderer.SetActive(true);
                if (Input.GetKey(KeyCode.Mouse0) && oldMouseGridPosition != mouseGridPosition) {
                    PlaceTile();
                }
            } else {
                brushRenderer.SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                oldMouseGridPosition = Vector3Int.forward;
            }
        }
    }

    public void SetBrush(GameObject tileBrush) {
        brushRenderer.SetActive(false);
        brushRenderer = tileBrush;
        brushRenderer.SetActive(true);
        activeBrush = brushRenderer.GetComponent<BaseBrush>();
        activeBrush.brushManager = this;
    }

    void PlaceTile() {
        activeBrush.Place();
        oldMouseGridPosition = mouseGridPosition;
    }

}