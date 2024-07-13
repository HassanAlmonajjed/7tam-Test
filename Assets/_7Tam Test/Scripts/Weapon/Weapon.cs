using UnityEngine;
using UnityEngine.Pool;


public class Weapon : MonoBehaviour
{
    [SerializeField] private Ammo _ammoPrefab;

    public IObjectPool<Ammo> _ammoPool;
    private Transform _muzzlePoint;
    private const int MAX_POOL_SIZE = 10;

    private void Awake()
    {
        _ammoPool = new ObjectPool<Ammo>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, MAX_POOL_SIZE);
        _muzzlePoint = transform.Find("Muzzle Point"); 
    }

    public void Fire()
    {
        Ammo ammo = _ammoPool.Get();
        ammo.transform.SetPositionAndRotation(
            _muzzlePoint.position,
            transform.root.rotation);

        ammo.Weapon = this;
    }

    public void RemoveAmmo(Ammo ammo)
    {
        _ammoPool.Release(ammo);
    }

    private void OnDestroyPoolObject(Ammo ammo) => Destroy(ammo.gameObject);

    private void OnReturnedToPool(Ammo ammo) => ammo.gameObject.SetActive(false);

    private void OnTakeFromPool(Ammo ammo) => ammo.gameObject.SetActive(true);

    private Ammo CreatePooledItem() => Instantiate(_ammoPrefab);
}

