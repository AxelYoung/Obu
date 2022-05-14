using UnityEngine;

public class Movement : MonoBehaviour {
    [System.NonSerialized] public bool isMoving = false;
    float moveSpeed = 10;
    Vector2 moveDirection;

    public void Move(Vector2 moveDirection) {
        if (!isMoving) {
            if (moveDirection == Vector2.zero) return;
            this.moveDirection = moveDirection;
            isMoving = true;
        } else {
            transform.position += (Vector3)this.moveDirection * Time.deltaTime * moveSpeed;
            if (hitWall()) StopMoving();
        }
    }

    bool hitWall() {
        if (Physics2D.Raycast(transform.position + (Vector3)(moveDirection / 2.01f), moveDirection, 0.05f, LayerMask.GetMask("Wall"))) return true;
        else return false;
    }

    public void StopMoving() {
        transform.position = GridFunctions.Vector2ToVector3Int(transform.position);
        moveDirection = Vector2.zero;
        isMoving = false;
    }
}
