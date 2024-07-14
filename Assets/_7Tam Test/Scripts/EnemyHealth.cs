using UnityEngine;

namespace SevenTamTest
{
    public class EnemyHealth : MonoBehaviour, IDamegable
    {
        [SerializeField] private int _health;
        [SerializeField] private GameObject _bloodVFX;
        [SerializeField] private Sprite _guts;

        public int Health => _health;

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 0)
                Death();
        }

        private void Death()
        {
            GetComponent<SpriteRenderer>().sprite = _guts;
            GameObject bloodVfx = Instantiate(_bloodVFX, transform.position, Quaternion.identity);
            Destroy(bloodVfx, 2);
        }
    }
}
