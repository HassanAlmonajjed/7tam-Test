using System.Collections;
using UnityEngine;

namespace SevenTamTest
{
    public class Machinegun : Weapon
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

            if (Time.time < _nextFireTime)
                return;

            _nextFireTime = Time.time + (1 / _fireRate);

            StartCoroutine(FireRoutine());

            IEnumerator FireRoutine()
            {
                _isFiring = true;

                for (int i = 0; i < _burstAmount; i++)
                {
                    GetAmmo();

                    StartCoroutine(InstantiateMuzzleFlashVFX());

                    yield return _waitBetweenBullets;
                }


                _isFiring = false;

                void GetAmmo()
                {
                    Ammo ammo = _ammoPool.Get();
                    ammo.transform.SetPositionAndRotation(
                        _muzzlePoint.position,
                        transform.root.rotation);

                    ammo.Weapon = this;
                }
            }
        }
    }
}
