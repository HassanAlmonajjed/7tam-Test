using System.Collections;
using UnityEngine;

namespace SevenTamTest
{
    public class EnemyHealth : MonoBehaviour, IDamegable
    {
        [SerializeField] private int _health;
        [SerializeField] private GameObject _bloodVFX;
        [SerializeField] private AudioClip[] _deathSFX;
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private Transform _fillAmountBar;

        public bool IsDead { get; private set; }
        public int Health => _currentHealth;

        private int _currentHealth;

        private IEnumerator _showHealthBarRoutine;

        private void Awake()
        {
            _healthBar.SetActive(false);
            _currentHealth = _health;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;

            _currentHealth -= damage;

            

            if (_currentHealth <= 0)
            {
                _healthBar.SetActive(false);
                Death();
            }

            else
            {
                if(_showHealthBarRoutine != null)
                    StopCoroutine(_showHealthBarRoutine);

                _showHealthBarRoutine = ShowHealthBar();
                StartCoroutine(_showHealthBarRoutine);
            }

            IEnumerator ShowHealthBar()
            {
                _healthBar.SetActive(true);
                float fillAmount = Mathf.Clamp((float)_currentHealth / _health, 0, _health);
                _fillAmountBar.localScale = new Vector3(fillAmount, 1, 1);
                yield return new WaitForSeconds(1);
                _healthBar.SetActive(false);
            }
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
