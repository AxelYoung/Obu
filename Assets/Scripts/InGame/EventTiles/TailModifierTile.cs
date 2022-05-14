using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailModifierTile : MonoBehaviour, IEventTile {

    public int tailAmountModifier;

    bool eventOccurred = false;

    Transform playerTransform;

    public void Event(Player player) {
        if (!eventOccurred) {
            playerTransform = player.transform;
            if (player.tailGeneration.maxTailLength + tailAmountModifier >= 0) {
                player.tailGeneration.maxTailLength += tailAmountModifier;
                eventOccurred = true;
            } else if (player.tailGeneration.maxTailLength != 0) {
                player.tailGeneration.maxTailLength = 0;
                eventOccurred = true;
            }
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
