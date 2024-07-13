using UnityEngine;

namespace SevenTamTest
{
    public class Pistol : Weapon
    {
        public override void Fire()
        {
            if (Time.time < _nextFireTime)
                return;

            GetAmmo();
            _nextFireTime = Time.time + (1 / _fireRate);
        }
    }
}
