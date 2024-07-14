using UnityEngine;

namespace SevenTamTest
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ice : MonoBehaviour, IDamegable
    {
        [SerializeField] private int _health;
        [SerializeField] private Sprite[] _healthSprites;
        [SerializeField] private GameObject _destructionEffect;

        private SpriteRenderer _spriteRenderer;

        private int _currentHealth;
        private bool _wasDestroyed;
        public int Health => _health;

        private void Awake()
        {
            _currentHealth = _health;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void TakeDamage(int damage)
        {
            if (_wasDestroyed) 
                return;

            _currentHealth -= damage;
            if (_currentHealth > 0)
                UpdateSprite();
            else
                DestroyIce();
        }

        void UpdateSprite()
        {
            int spriteIndex = _currentHealth * _healthSprites.Length / _health;
            spriteIndex = Mathf.Clamp(spriteIndex, 0, _healthSprites.Length - 1);

            _spriteRenderer.sprite = _healthSprites[spriteIndex];
        }

        void DestroyIce()
        {
            _wasDestroyed = true;
            GameObject vfx = Instantiate(_destructionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(vfx, 1);
        }
    }
}
