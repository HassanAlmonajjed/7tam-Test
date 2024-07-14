using UnityEngine;

namespace SevenTamTest
{
    public class Shotgun : Weapon
    {
        [Header("Shotgun")]
        [SerializeField] private int _burstAmount;

        [Range(45, 180f)]
        [SerializeField] private int _range;

        public override void Fire()
        {
            if (Time.time < _nextFireTime)
                return;

            _nextFireTime = Time.time + (1 / _fireRate);

            float angleStep = _range / (_burstAmount - 1);
            float startAngle = (-_range / 2) + 90;

            for (int i = 0; i < _burstAmount; i++)
            {
                float currentAngle = startAngle + i * angleStep;

                float currentAngleRad = currentAngle * Mathf.Deg2Rad;

                Vector3 direction = new(Mathf.Cos(currentAngleRad), Mathf.Sin(currentAngleRad), 0);
                direction = transform.root.rotation * direction;
                Ammo ammo = _ammoPool.Get();

                ammo.transform.position = _muzzlePoint.position;
                ammo.transform.up = direction;

                ammo.Weapon = this;
            }
        }
    }
}
