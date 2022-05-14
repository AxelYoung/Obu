using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

    public Vector2 moveDirection { get; private set; }

    PlayerInput playerInput;

    SpriteRenderer spriteRenderer;

    void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    public void GetInput() {
        if (playerInput.actions["Reset"].WasPressedThisFrame()) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Vector2 inputDirection = playerInput.actions["Move"].ReadValue<Vector2>();
        if (inputDirection.magnitude > 1) inputDirection = Vector2.zero;
        moveDirection = inputDirection;
    }

}
