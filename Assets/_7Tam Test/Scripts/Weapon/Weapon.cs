using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] private Ammo _ammoPrefab;
    private Transform _muzzlePoint;

    private void Awake()
    {
        _muzzlePoint = transform.Find("Muzzle Point"); 
    }

    public void Fire()
    {
        Ammo ammo = Instantiate(_ammoPrefab);
        ammo.transform.SetPositionAndRotation(
            _muzzlePoint.position,
            transform.root.rotation);
    }
}

