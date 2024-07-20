using System.Collections;
using UnityEngine;

namespace SevenTamTest
{
    public class Pistol : Weapon
    {
        public override void Fire()
        {
            if (Time.time < _nextFireTime)
                return;

            Ammo ammo = InstantiateAmmo();
            ammo.Weapon = this;

            StartCoroutine(InstantiateMuzzleFlashVFX());

            _nextFireTime = Time.time + (1 / _fireRate);

            Ammo InstantiateAmmo()
            {
                Ammo ammo = _ammoPool.Get();

                ammo.transform.SetPositionAndRotation(
                    _muzzlePoint.position,
                    transform.root.rotation);
                return ammo;
            }
        }
    }
}
