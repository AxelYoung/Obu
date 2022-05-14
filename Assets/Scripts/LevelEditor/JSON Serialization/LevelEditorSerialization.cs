using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEditorSerialization : MonoBehaviour {

    [SerializeField] Tilemap tilemap;
    [SerializeField] BrushManager brushManager;


    [SerializeField] TileBase playerTile;
    [SerializeField] TileBase goalTile;
    [SerializeField] TileBase wallTile;
    [SerializeField] TileBase tailModifierTile;
    [SerializeField] GameObject tailModiferUI;
    [SerializeField] Canvas worldCanvas;

    [SerializeField] Text levelNameText;
    [SerializeField] Text playerTailLengthText;

    LevelData levelData = new LevelData();
    List<Vector3> boundsData = new List<Vector3>();
    bool playerInstance = false;
    bool goalInstance = false;

    void Start() { if (LevelIDs.levelName != "") LoadJSONInLevelEditor(); }

    public void SaveLevelAsJSON() {
        if (levelNameText.text == "" || playerTailLengthText.text == "") return;

        string levelName = levelNameText.text + ".json";
        int playerAmount = int.Parse(playerTailLengthText.text);
        int gridEdge = (int)brushManager.gridBounds.size.x / 2;

        for (int x = -gridEdge; x <= gridEdge; x++) {
            for (int y = -gridEdge; y <= gridEdge; y++) {
                Vector3Int tilePosition = tilemap.WorldToCell(new Vector2(x, y));
                TileBase tile = tilemap.GetTile(tilePosition);
                int tileID = LevelIDs.GetTileID(tile);
                if (tile == wallTile) continue;

                if (tile == tailModifierTile) levelData.tileData.Add(new TileData(tile, tilePosition, tileID, GetAmountInfoFromPosition(tilePosition)));
                else if (tile == playerTile) {
                    levelData.tileData.Add(new TileData(tile, tilePosition, tileID, playerAmount));
                    playerInstance = true;
                } else levelData.tileData.Add(new TileData(tile, tilePosition, tileID));

                if (tile == goalTile) goalInstance = true;
                boundsData.Add(tilePosition);
            }
        }

        if (!goalInstance || !playerInstance) return;

        levelData.bounds = GeometryUtility.CalculateBounds(boundsData.ToArray(), Matrix4x4.identity);
        FileFunctions.WriteFile(FileFunctions.GetPath(levelName), JsonUtility.ToJson(levelData));
        SceneManager.LoadScene("MainMenu");
    }

    public int GetAmountInfoFromPosition(Vector3Int tilePosition) {
        int amount = 0;
        if (brushManager.tailModifierUIs.ContainsKey(tilePosition)) {
            amount = brushManager.tailModifierUIs[tilePosition].CalculateAmount();
            brushManager.tailModifierUIs.Remove(tilePosition);
        }
        return amount;
    }

    public void LoadJSONInLevelEditor() {
        LevelData levelData = FileFunctions.GetLevelDataJSON();
        foreach (TileData tileData in levelData.tileData) {
            TileBase tileBase = LevelIDs.tileBases[tileData.tileID];
            SetDefaultPlayerAmountText(tileData, tileBase);
            CreateTailModifierUI(tileData, tileBase);
            tilemap.SetTile(tileData.position, tileBase);
        }
        ForceDefaultFileName();
    }

    void SetDefaultPlayerAmountText(TileData tileData, TileBase tileBase) {
        if (tileBase == playerTile) {
            playerTailLengthText.transform.parent.GetComponent<InputField>().text = tileData.amount.ToString();
        }
    }

    void CreateTailModifierUI(TileData tileData, TileBase tileBase) {
        if (tileBase == tailModifierTile) {
            TailModifierUI currentUI = Instantiate(tailModiferUI, tilemap.CellToWorld(tileData.position) + new Vector3(0.5f, 0.5f), Quaternion.identity, worldCanvas.transform).GetComponent<TailModifierUI>();
            brushManager.tailModifierUIs.Add(tileData.position, currentUI);
            currentUI.SetAmount(tileData.amount);
        }
    }

    void ForceDefaultFileName() {
        levelNameText.transform.parent.GetComponent<InputField>().text = LevelIDs.levelName.Substring(0, LevelIDs.levelName.Length - 5);
        levelNameText.transform.parent.GetComponent<InputField>().interactable = false;
    }
}