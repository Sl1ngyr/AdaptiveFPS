using System.Collections.Generic;
using Bullet;
using UnityEngine;
using UnityEngine.Pool;

namespace Enemy
{
    public class BlueEnemyObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private EnemyBulletSpawner spawnBullet;
        [SerializeField] private BlueEnemy blueEnemyPrefab;
        
        [Space]
        [SerializeField] private int maxSize;
        [SerializeField] private int defaultCapacity;
        [SerializeField] private bool collectionCheck;
        
        private float boundsY;
        private List<BlueEnemy> listBlueEnemy;
        
        public IObjectPool<BlueEnemy> pool;

        private void Awake()
        {
            pool = new ObjectPool<BlueEnemy>(
                CreateBlueEnemy,
                OnGet,
                OnRelease,
                onDestroyBlueEnemy, collectionCheck,
                defaultCapacity,
                maxSize);
            listBlueEnemy = new List<BlueEnemy>();
        }
        
        private BlueEnemy CreateBlueEnemy()
        {
            var blueEnemy = Instantiate(blueEnemyPrefab, transform.position, Quaternion.identity);
            blueEnemy.Init(target, spawnBullet);
            blueEnemy.Pool = pool;
            
            return blueEnemy;
        }
        
        private void OnGet(BlueEnemy blueEnemy)
        {
            blueEnemy.transform.position = transform.position;
            blueEnemy.transform.rotation = transform.rotation;
            
            boundsY = blueEnemy.GetBoundsY();
            
            blueEnemy.gameObject.SetActive(true);
            blueEnemy.Pool = pool;
            listBlueEnemy.Add(blueEnemy);
        }
        
        private void OnRelease(BlueEnemy blueEnemy)
        {
            blueEnemy.gameObject.SetActive(false);
        }
        
        private void onDestroyBlueEnemy(BlueEnemy blueEnemy)
        {
            Destroy(blueEnemy.gameObject);
        }

        public float GetBlueEnemyBoundsY()
        {
            return boundsY;
        }

        public int GetCountActiveBlueEnemy()
        {
            int count = 0;
            
            foreach (var enemy in listBlueEnemy)
            {
                if (enemy.isActiveAndEnabled == false) 
                    continue;
                
                count++;
            }
            
            return count;
        }

        public void DeactivateBlueEnemy()
        {
            foreach (var deactivate in listBlueEnemy)
            {
                if(deactivate.isActiveAndEnabled == false) 
                    continue;
                
                PlayerBullet.enemyKills++;
                deactivate.Deactivate();
            }
        }
    }
}