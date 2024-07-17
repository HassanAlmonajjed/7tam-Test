using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace SevenTamTest
{
    public class EnemyManager : MonoBehaviour
    {
        private List<GameObject> _enemies;

        private void Start()
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }

        public GameObject GetClosestEnemy(Vector2 position, float radius)
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (var enemy in _enemies)
            {
                if (enemy == null) 
                    continue; 

                float distance = Vector2.Distance(position, enemy.transform.position);
                if (distance < radius && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
    }
}
