using UnityEngine;

namespace SevenTamTest.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Boundary _boundary;
        private Rigidbody2D _rigidbody;
        private PlayerInput _input;
        private Animator _animator;

        private readonly int IS_MOVING_HASH = Animator.StringToHash("isMoving");

        private void Awake()
        {
            ResloveDependencies();
        }

        private void Update()
        {
            bool isMoving = _input.Movement != Vector2.zero;
            UpdatePlayerLegsAnimation(isMoving);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void UpdatePlayerLegsAnimation(bool isMoving) => _animator.SetBool(IS_MOVING_HASH, isMoving);

        private void ResloveDependencies()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            _input = FindObjectOfType<PlayerInput>();
        }

        private void Move()
        {
            Vector2 currentPosition = _rigidbody.position;

            currentPosition += _speed * Time.fixedDeltaTime * _input.Movement;

            currentPosition.x = Mathf.Clamp(currentPosition.x, _boundary.MinX, _boundary.MaxX);
            currentPosition.y = Mathf.Clamp(currentPosition.y, _boundary.MinY, _boundary.MaxY);

            _rigidbody.MovePosition(currentPosition);
        }
    }

    [System.Serializable]
    public class Boundary
    {
        public float MinX;
        public float MaxX;
        public float MinY;
        public float MaxY;
    }
}