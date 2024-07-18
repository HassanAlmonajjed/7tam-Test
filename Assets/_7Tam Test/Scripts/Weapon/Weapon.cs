using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

namespace SevenTamTest
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _fireRate;
        [field: SerializeField] public bool IsSingleHanded { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }

        protected Transform _muzzlePoint;
        protected float _nextFireTime;
        private const int MAX_POOL_SIZE = 10;

        protected virtual void Awake()
        {
            _ammoPool = new ObjectPool<Ammo>(CreateAmoPooledItem, OnTakeFromAmmoPool, OnReturnedToAmmoPool, OnDestroyAmmoPoolObject, false, 10, MAX_POOL_SIZE);
            _muzzleFlashPool = new ObjectPool<GameObject>(CreateMuzzleFlashPooledItem, OnTakeFromMuzzleFlashPool, OnReturnedToMuzzleFlashPool, OnDestroyMuzzleFlashPoolObject, false, 10, MAX_POOL_SIZE);
            _muzzlePoint = transform.Find("Muzzle Point"); // the position where a bullet will be instantiated
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Handles.DrawWireDisc(transform.position, transform.forward, Radius);
        }

        public abstract void Fire();

        public void RemoveAmmo(Ammo ammo)
        {
            _ammoPool.Release(ammo);
        }

        #region Ammo Pool

        [Header("Weapon")]
        [SerializeField] protected Ammo _ammoPrefab;
        protected IObjectPool<Ammo> _ammoPool;

        private void OnDestroyAmmoPoolObject(Ammo ammo) => Destroy(ammo.gameObject);

        private void OnReturnedToAmmoPool(Ammo ammo) => ammo.gameObject.SetActive(false);

        private void OnTakeFromAmmoPool(Ammo ammo) => ammo.gameObject.SetActive(true);

        private Ammo CreateAmoPooledItem() => Instantiate(_ammoPrefab);

        #endregion

        #region Muzzle Flash Pool

        [SerializeField] protected GameObject _muzzleFlashPrefab;
        protected IObjectPool<GameObject> _muzzleFlashPool;

        protected IEnumerator InstantiateMuzzleFlashVFX()
        {
            GameObject vfx = _muzzleFlashPool.Get();
            vfx.transform.SetPositionAndRotation(
                _muzzlePoint.position,
                transform.rotation);

            yield return new WaitForSeconds(0.1f);

            _muzzleFlashPool.Release(vfx);
        }

        private GameObject CreateMuzzleFlashPooledItem() => Instantiate(_muzzleFlashPrefab, transform);

        private void OnDestroyMuzzleFlashPoolObject(GameObject muzzleFlashObject) => Destroy(muzzleFlashObject);

        private void OnReturnedToMuzzleFlashPool(GameObject muzzleFlashObject) => muzzleFlashObject.SetActive(false);

        private void OnTakeFromMuzzleFlashPool(GameObject muzzleFlashObject) => muzzleFlashObject.SetActive(true);

        #endregion
    }
}