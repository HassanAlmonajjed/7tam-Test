using UnityEngine;
using System.Collections;

namespace SevenTamTest
{
    public class Shotgun : Weapon
    {
        [Tooltip("the number of bullets fired at the same time when player hit fire button")]
        [SerializeField] private int _burstAmount;

        [SerializeField] private float _burstSpeed;

        private bool _isFiring;
        private WaitForSeconds _waitBetweenBullets;

        protected override void Awake()
        {
            base.Awake();

            _waitBetweenBullets = new(1 / _burstSpeed);
        }

        public override void Fire()
        {
            if (_isFiring)
                return;

            StartCoroutine(FireRoutine());

            IEnumerator FireRoutine()
            {
                _isFiring = true;

                for (int i = 0; i < _burstAmount; i++)
                {
                    GetAmmo();

                    yield return _waitBetweenBullets;
                }
                

                _isFiring = false;
            }
        }


    }
}
