using UnityEngine;

namespace SevenTamTest
{
    public class EnemyHealth : MonoBehaviour, IDamegable
    {
        [SerializeField] private int _health;
        [SerializeField] private GameObject _bloodVFX;
        [SerializeField] private AudioClip[] _deathSFX;

        public bool IsDead { get; private set; }

        public int Health => _health;

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;

            _health -= damage;

            if (_health < 0)
                Death();
        }

        private void Death()
        {
            IsDead = true;

            // stop the movement
            GetComponent<EnemyMovement>().enabled = false;

            GetComponent<Animator>().SetBool("IsDead", IsDead);

            Destroy(gameObject, 2);

            InstantiateBloodVFX();

            PlayDeathSFX();

            void PlayDeathSFX()
            {
                int randonIndex = Random.Range(0, _deathSFX.Length);
                AudioSource.PlayClipAtPoint(_deathSFX[randonIndex], Vector3.zero);
            }

            void InstantiateBloodVFX()
            {
                GameObject bloodVfx = Instantiate(_bloodVFX, transform.position, Quaternion.identity);
                Destroy(bloodVfx, 1);
            }
        }
    }
}
