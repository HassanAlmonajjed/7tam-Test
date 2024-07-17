using UnityEngine;

namespace SevenTamTest
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _target;

        private EnemyHealth _health;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            Move(direction);
            Rotate(direction);
        }

        private void Move(Vector2 direction)
        {
            Vector2 newPositon = (Vector2)transform.position + (_speed * Time.fixedDeltaTime * direction);
            _rigidbody.MovePosition(newPositon);
        }

        private void Rotate(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody.rotation = angle;
        }

    }
}