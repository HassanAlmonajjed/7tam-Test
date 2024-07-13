using UnityEngine;

namespace SevenTamTest.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private PlayerInput _input;
        private Animator _animator;

        private readonly int IS_MOVING_HASH = Animator.StringToHash("isMoving");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();
        }

        private void Update()
        {
            bool isMoving = _input.Movement != Vector2.zero;
            _animator.SetBool(IS_MOVING_HASH, isMoving);
        }

        private void FixedUpdate()
        {
            Vector2 currentPosition = _rigidbody.position;

            currentPosition += _speed * Time.fixedDeltaTime * _input.Movement;

            _rigidbody.MovePosition(currentPosition);
        }
    }
}