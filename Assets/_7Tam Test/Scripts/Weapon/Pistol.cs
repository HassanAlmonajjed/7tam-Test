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

            IEnumerator InstantiateMuzzleFlashVFX()
            {
                GameObject vfx = _muzzleFlashPool.Get();
                vfx.transform.SetPositionAndRotation(
                    _muzzlePoint.position,
                    transform.rotation);

                yield return new WaitForSeconds(0.1f);

                _muzzleFlashPool.Release(vfx);
            }
        }
    }
}
