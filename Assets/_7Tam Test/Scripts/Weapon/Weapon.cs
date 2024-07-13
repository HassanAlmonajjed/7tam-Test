using UnityEngine;
using UnityEngine.Pool;


public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Ammo _ammoPrefab;
    [field: SerializeField] public bool IsSingleHanded { get; private set; }
    protected IObjectPool<Ammo> _ammoPool;
    protected Transform _muzzlePoint;
    private const int MAX_POOL_SIZE = 10;

    protected virtual void Awake()
    {
        _ammoPool = new ObjectPool<Ammo>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, MAX_POOL_SIZE);
        _muzzlePoint = transform.Find("Muzzle Point"); 
    }

    public abstract void Fire();

    public void RemoveAmmo(Ammo ammo)
    {
        _ammoPool.Release(ammo);
    }

    protected void GetAmmo()
    {
        Ammo ammo = _ammoPool.Get();
        ammo.transform.SetPositionAndRotation(
            _muzzlePoint.position,
            transform.root.rotation);

        ammo.Weapon = this;
    }

    private void OnDestroyPoolObject(Ammo ammo) => Destroy(ammo.gameObject);

    private void OnReturnedToPool(Ammo ammo) => ammo.gameObject.SetActive(false);

    private void OnTakeFromPool(Ammo ammo) => ammo.gameObject.SetActive(true);

    private Ammo CreatePooledItem() => Instantiate(_ammoPrefab);
}

