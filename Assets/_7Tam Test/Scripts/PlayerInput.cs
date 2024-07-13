using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Movement {  get; private set; }

    public void OnMove(InputValue input) => Movement = input.Get<Vector2>();
}
