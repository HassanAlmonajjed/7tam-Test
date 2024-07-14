using UnityEngine;

namespace SevenTamTest
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ammo : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        private Rigidbody2D _rigidbody;
        public Weapon Weapon { get; set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamegable damegable))
                damegable.TakeDamage(_damage);

            Weapon.RemoveAmmo(this);
        }

        private void Move()
        {
            Vector3 newPosition = transform.position + (_speed * Time.fixedDeltaTime * transform.up);
            _rigidbody.MovePosition(newPosition);
        }
    }
}