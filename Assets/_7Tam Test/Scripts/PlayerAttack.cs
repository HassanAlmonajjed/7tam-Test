using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Weapon _defaultWeapon;

    private Weapon _currentWeapon;

    private void Awake()
    {
        _currentWeapon = _defaultWeapon;
    }

    public void Fire()
    {
        _currentWeapon.Fire();
    }

    public void SwitchWeapon(Weapon newWeapon)
    {
        _currentWeapon = newWeapon;
    }
}
