using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGeneration : MonoBehaviour {

    [SerializeField] GameObject tailPrefab;
    public int maxTailLength;
    [SerializeField] AudioClip tailSpawnAudio;
    [System.NonSerialized] public Transform gridTransform;
    public List<GameObject> tailTiles { get; private set; }
    float tailSpawnDistance = 0.2f;
    Vector2 tailSpawnPosition;
    AudioSource audioSource;

    public Sprite wideTurnTailSprite;
    public Sprite tightTurnTailSprite;

    Vector2 previousMoveDir = Vector2.zero;
    GameObject tailEnd;
    public Sprite tailEndSprite;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        gridTransform = GameObject.FindObjectOfType<Grid>().transform;
        tailTiles = new List<GameObject>();
    }

    void Start() {
        tailSpawnPosition = transform.position;
    }

    public void GenerateTail(Vector2 moveDirection) {
        if (GridFunctions.Vector2PassedPoint(transform.position, tailSpawnPosition + (moveDirection * tailSpawnDistance), moveDirection)) AddTailTile(moveDirection);
        if (tailTiles.Count > maxTailLength) RemoveTailTile(tailTiles[0]);
    }

    void AddTailTile(Vector2 moveDirection) {
        bool first = false;
        if (previousMoveDir != moveDirection) {
            transform.right = moveDirection;
            first = true;
        }
        if (maxTailLength > 0) {
            audioSource.PlayOneShot(tailSpawnAudio);
            tailTiles.Add(newTailTile(moveDirection, first));
        }
        previousMoveDir = moveDirection;
        tailSpawnPosition += moveDirection;
        UpdateTailEnd();
    }

    GameObject newTailTile(Vector2 moveDirection, bool first) {
        GameObject currentTail = Instantiate(tailPrefab, GridFunctions.Vector2ToVector3Int(tailSpawnPosition), Quaternion.identity, gridTransform);
        if (first) currentTail.GetComponent<SpriteRenderer>().sprite = moveDirection == new Vector2(-previousMoveDir.y, previousMoveDir.x) ? wideTurnTailSprite : tightTurnTailSprite;
        currentTail.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg, Vector3.forward);
        return currentTail;
    }

    void RemoveTailTile(GameObject tailTile) {
        Destroy(tailTile);
        tailTiles.Remove(tailTile);
        UpdateTailEnd();
    }

    void UpdateTailEnd() {
        if (tailTiles.Count == 0) return;
        if (tailEnd == tailTiles[0]) return;
        tailTiles[0].GetComponent<SpriteRenderer>().sprite = tailEndSprite;
        tailEnd = tailTiles[0];
    }

    public int NewestTailTileFromList(List<GameObject> tileList) {
        int index = 0;
        foreach (GameObject tailTile in tileList) {
            if (tailTiles.Contains(tailTile)) {
                if (tailTiles.IndexOf(tailTile) > index) {
                    index = tailTiles.IndexOf(tailTile);
                }
            }
        }
        return index;
    }

    public void RemoveTailTilesBeforeIndex(int index) {
        for (int i = 0; i <= index; i++) {
            RemoveTailTile(tailTiles[0]);
        }
    }
}
