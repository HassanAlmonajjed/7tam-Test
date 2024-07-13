using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private PlayerInput _input;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = _rigidbody.position;

        currentPosition += _speed * Time.fixedDeltaTime * _input.Movement;

        _rigidbody.MovePosition(currentPosition);
    }
}
