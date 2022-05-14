using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTile : MonoBehaviour, IEventTile {

    public Sprite switchOffSprite;
    public Sprite switchOnSprite;
    public Sprite spikeOffSprite;
    public Sprite spikeOnSprite;

    SwitchTile[] switches;
    GameObject[] blocks;

    bool eventOccurred;

    public bool state;

    Transform playerTransform;

    List<GameObject> hits = new List<GameObject>();

    void Start() {
        switches = GameObject.FindObjectsOfType<SwitchTile>();
        blocks = GameObject.FindGameObjectsWithTag("SwitchWall");
    }

    public void Event(Player player) {
        if (!eventOccurred) {
            playerTransform = player.transform;
            state = !state;
            foreach (SwitchTile switchObj in switches) {
                switchObj.GetComponent<SpriteRenderer>().sprite = state ? switchOnSprite : switchOffSprite;
                switchObj.state = state;
            }
            foreach (GameObject block in blocks) {
                if (!state) {
                    RaycastHit2D hit = Physics2D.Raycast(block.transform.position, Vector2.up, 0.01f);
                    if (hit.transform != false) {
                        hits.Add(hit.transform.gameObject);
                    }
                }
                block.GetComponent<BoxCollider2D>().enabled = !state;
                block.GetComponent<SpriteRenderer>().sprite = state ? spikeOnSprite : spikeOffSprite;
            }
            if (hits.Count != 0) {
                player.tailGeneration.RemoveTailTilesBeforeIndex(player.tailGeneration.NewestTailTileFromList(hits));
            }
            hits.Clear();
            eventOccurred = true;
        }
    }

    void Update() {
        if (eventOccurred) {
            if (Vector2.Distance(transform.position, playerTransform.position) >= 1) {
                eventOccurred = false;
            }
        }
    }
}
