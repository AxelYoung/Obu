using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTile : MonoBehaviour, IEventTile {
    bool triggered = false;
    bool eventOccurred = false;

    Player player;
    Transform playerTransform;

    public void Event(Player player) {
        if (!triggered && !eventOccurred) {
            this.player = player;
            playerTransform = player.transform;
            triggered = true;
        }
    }

    void Update() {
        if (eventOccurred) {
            if (Vector2.Distance(transform.position, playerTransform.position) >= 1) {
                eventOccurred = false;
            }
        } else if (triggered) {
            if (GridFunctions.Vector2PassedPoint(playerTransform.position, transform.position, player.inputManager.moveDirection)) {
                triggered = false;
                eventOccurred = true;
                player.movement.StopMoving();
            }
        }
    }
}
