using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour {

    public InputManager inputManager { get; private set; }
    public Movement movement { get; private set; }
    public TailGeneration tailGeneration { get; private set; }


    LevelLoader levelLoader;
    bool canMove = true;

    void Awake() {
        inputManager = GetComponent<InputManager>();
        movement = GetComponent<Movement>();
        tailGeneration = GetComponent<TailGeneration>();
        levelLoader = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<LevelLoader>();
        levelLoader.LevelLoaded += (() => { canMove = false; StartCoroutine(DelayedEnableMove()); });
        levelLoader.LevelExit += (() => { canMove = false; });
    }

    void Update() {
        if (!canMove) return;
        if (!movement.isMoving) {
            inputManager.GetInput();
        }
        movement.Move(inputManager.moveDirection);
        tailGeneration.GenerateTail(inputManager.moveDirection);
        CheckForEventTile();
    }

    IEnumerator DelayedEnableMove() {
        yield return new WaitForSeconds(levelLoader.animationLength);
        canMove = true;
    }

    void CheckForEventTile() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)(inputManager.moveDirection / 2.01f), inputManager.moveDirection, .001f, LayerMask.GetMask("Event"));
        if (hit.transform != null) hit.transform.GetComponent<IEventTile>().Event(this);
    }
}
