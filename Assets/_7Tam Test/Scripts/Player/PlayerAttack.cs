using UnityEngine;
using System.Collections.Generic;

namespace SevenTamTest.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _allWeapons;
        [SerializeField] private Sprite _singleHand;
        [SerializeField] private Sprite _dualHand;
        private Transform _weaponRadiusModel;

        private EnemyManager _enemyManager;
        private GameObject _closestTarget;

        private SpriteRenderer _spriteRenderer;

        private Weapon _currentWeapon;

        private void Awake()
        {
            _spriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
            _weaponRadiusModel = transform.Find("Weapon Radius");
        }

        private void Start()
        {
            _enemyManager = FindObjectOfType<EnemyManager>();
            SwitchWeapon(0);

            
        }

        private void Update()
        {
            _closestTarget = _enemyManager.GetClosestEnemy(transform.position, _currentWeapon.Radius);

            if (_closestTarget)
            {
                Vector3 relitivePositon = transform.InverseTransformPoint(_closestTarget.transform.position);
                float angle = Mathf.Atan2(relitivePositon.y, relitivePositon.x) * Mathf.Rad2Deg - 90;
                transform.Rotate(0, 0, angle);
            }
        }

        public void Fire() => _currentWeapon.Fire();

        public void SwitchWeapon(int weaponIndex)
        {
            ActivateWeapon(weaponIndex);

            SwitchBody();

            _weaponRadiusModel.localScale = _currentWeapon.Radius * 2 * Vector3.one;

            void SwitchBody() => _spriteRenderer.sprite = _currentWeapon.IsSingleHanded ? _singleHand : _dualHand;

            void ActivateWeapon(int index)
            {
                _currentWeapon = _allWeapons[index];

                for (int i = 0; i < _allWeapons.Count; i++)
                    _allWeapons[i].gameObject.SetActive(i == index);
            }
        }
    }
}