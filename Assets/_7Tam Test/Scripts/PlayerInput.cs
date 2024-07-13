using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 _moveInput;

    public void OnMove(InputValue input) => _moveInput = input.Get<Vector2>();
}
