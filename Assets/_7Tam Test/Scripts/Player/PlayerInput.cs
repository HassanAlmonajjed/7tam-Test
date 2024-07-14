using UnityEngine;
using UnityEngine.InputSystem;

namespace SevenTamTest.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 Movement { get; private set; }
        private _7tamTest _playerControl;
        private InputAction _move;

        private void Awake()
        {
            _playerControl = new _7tamTest();
            _move = _playerControl.Player.Move;
        }

        private void OnEnable()
        {
            _playerControl.Enable();
        }

        private void OnDisable()
        {
            _playerControl.Disable();
        }

        private void Update()
        {
            Movement = _move.ReadValue<Vector2>();
        }
    }
}