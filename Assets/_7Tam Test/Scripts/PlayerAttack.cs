using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<Weapon> _allWeapons;
    [SerializeField] private Sprite _singleHand;
    [SerializeField] private Sprite _dualHand;

    private SpriteRenderer _spriteRenderer;

    private Weapon _currentWeapon;

    private void Awake()
    {
        _spriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SwitchWeapon(0);
    }

    public void Fire() => _currentWeapon.Fire();

    public void SwitchWeapon(int weaponIndex)
    {
        ActivateWeapon(weaponIndex);

        SwitchBody();

        void SwitchBody() => _spriteRenderer.sprite = _currentWeapon.IsSingleHanded ? _singleHand : _dualHand;
        
        void ActivateWeapon(int index)
        {
            _currentWeapon = _allWeapons[index];

            for (int i = 0; i < _allWeapons.Count; i++)
                _allWeapons[i].gameObject.SetActive(i == index);
        }
    }
}
