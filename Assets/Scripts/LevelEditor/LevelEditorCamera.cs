using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class LevelEditorCamera : MonoBehaviour {

    [SerializeField] float zoomSpeed;

    [SerializeField] float minSize;
    float maxSize;

    [SerializeField] float dragSpeed;
    Vector3 dragOrigin;

    Camera camera;

    void Awake() {
        camera = GetComponent<Camera>();
        maxSize = GameObject.FindGameObjectWithTag("Grid").GetComponent<CreateGrid>().gridSize / 2f;
    }

    void Update() {
        Zoom();
        Drag();
    }

    void Zoom() {
        if (Input.mouseScrollDelta.y == 0) return;
        camera.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minSize, maxSize);
        CheckBounds();
    }

    void Drag() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) dragOrigin = Input.mousePosition;
        if (!Input.GetKey(KeyCode.Mouse1)) return;
        Vector3 pos = camera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
        transform.Translate(move * (camera.orthographicSize / maxSize), Space.World);
        CheckBounds();
    }

    void CheckBounds() {
        Vector2 bounds = new Vector2(maxSize - (camera.orthographicSize / maxSize), maxSize - (camera.orthographicSize / maxSize));
        float xBound = Mathf.Clamp(transform.position.x, -bounds.x, bounds.x);
        float yBound = Mathf.Clamp(transform.position.y, -bounds.y, bounds.y);
        transform.position = new Vector3(xBound, yBound, -10);
    }
}
