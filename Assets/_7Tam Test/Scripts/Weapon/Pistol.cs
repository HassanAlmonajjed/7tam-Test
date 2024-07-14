using UnityEngine;

namespace SevenTamTest
{
    public class Pistol : Weapon
    {
        public override void Fire()
        {
            if (Time.time < _nextFireTime)
                return;

            Ammo ammo = _ammoPool.Get();
            ammo.transform.SetPositionAndRotation(
                _muzzlePoint.position,
                transform.root.rotation);

            ammo.Weapon = this;

            _nextFireTime = Time.time + (1 / _fireRate);
        }
    }
}
