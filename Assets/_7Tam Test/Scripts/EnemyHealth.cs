using UnityEngine;

namespace SevenTamTest
{
    public class EnemyHealth : MonoBehaviour, IDamegable
    {
        [SerializeField] private int _health;
        [SerializeField] private GameObject _bloodVFX;

        private bool _isDead;
        public int Health => _health;

        public void TakeDamage(int damage)
        {
            if (_isDead)
                return;

            _health -= damage;

            if (_health < 0)
                Death();
        }

        private void Death()
        {
            _isDead = true;

            GetComponent<Animator>().SetBool("IsDead", _isDead);
            Destroy(gameObject, 2);
            GameObject bloodVfx = Instantiate(_bloodVFX, transform.position, Quaternion.identity);
            Destroy(bloodVfx, 1);
        }
    }
}
