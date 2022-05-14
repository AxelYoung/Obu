using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : MonoBehaviour, IEventTile {

    LevelLoader levelLoader;

    void Awake() {
        levelLoader = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<LevelLoader>();
    }

    public void Event(Player player) {
        if (!player.movement.isMoving) {
            levelLoader.LoadMainMenu();
        }
    }
}
