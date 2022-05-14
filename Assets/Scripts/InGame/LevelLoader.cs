using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public TileGameObject[] tileGameObjects;

    Dictionary<TileBase, GameObject> tileToGameobject = new Dictionary<TileBase, GameObject>();

    public Tilemap floormap;
    public Tilemap wallmap;

    public Transform cameraTransform;

    public TileBase wallTile;
    public TileBase floorTile;
    public int padding = 10;

    public TileBase amountTile;
    public TileBase offSwitchTile;
    public TileBase onSwitchTile;
    public TileBase playerTile;

    public float animationLength = 1;
    public event Action LevelLoaded;
    public event Action LevelExit;

    public Player player;

    AudioSource audioSource;
    public AudioClip audioClip;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        foreach (TileGameObject tileGameObject in tileGameObjects) {
            tileToGameobject.Add(tileGameObject.tile, tileGameObject.prefab);
        }
        ConvertJson();
    }

    public void ConvertJson() {
        LevelData levelData = JsonUtility.FromJson<LevelData>(FileFunctions.GetFile(FileFunctions.GetPath(LevelIDs.levelName)));
        cameraTransform.position = new Vector3(levelData.bounds.center.x, levelData.bounds.center.y, cameraTransform.transform.position.z);
        int extentX = Mathf.RoundToInt(levelData.bounds.extents.x) + padding;
        int extentY = Mathf.RoundToInt(levelData.bounds.extents.y) + padding;
        if (extentX * (9f / 16f) < extentY) {
            extentX = Mathf.RoundToInt(extentY * (16f / 9f));
        } else {
            extentY = Mathf.RoundToInt(extentX * (9f / 16f));
        }
        for (int x = -extentX; x <= extentX; x++) {
            for (int y = -extentY; y <= extentY; y++) {
                wallmap.SetTile(wallmap.WorldToCell(levelData.bounds.center + new Vector3(x, y)), wallTile);
            }
        }
        wallmap.CompressBounds();
        cameraTransform.GetComponent<Camera>().orthographicSize = (wallmap.localBounds.extents.x > wallmap.localBounds.extents.y ? wallmap.localBounds.extents.x : wallmap.localBounds.extents.y) / 2f;
        foreach (TileData tile in levelData.tileData) {
            TileBase tileBase = LevelIDs.tileBases[tile.tileID];
            wallmap.SetTile(tile.position, null);
            floormap.SetTile(tile.position, floorTile);
            if (tileToGameobject.ContainsKey(tileBase)) {
                GameObject currentTile = Instantiate(tileToGameobject[tileBase].gameObject, wallmap.CellToWorld(tile.position) + new Vector3(.5f, .5f, 0), Quaternion.identity);
                if (tileBase == amountTile) {
                    currentTile.GetComponent<TailModifierTile>().tailAmountModifier = tile.amount;
                }
                if (tileBase == playerTile) {
                    player = currentTile.GetComponent<Player>();
                    currentTile.GetComponent<TailGeneration>().maxTailLength = tile.amount;
                }
            }
        }
        LevelLoaded?.Invoke();
    }

    public void LoadMainMenu() {
        LevelExit?.Invoke();
        audioSource.PlayOneShot(audioClip);
        StartCoroutine(LoadMenuDelay());
    }

    IEnumerator LoadMenuDelay() {
        yield return new WaitForSeconds(animationLength);
        SceneManager.LoadScene("MainMenu");
    }
}