using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using Bullet;

namespace Enemy
{
    public class FlyRedEnemyObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private BaseFlyRedEnemy flyRedEnemyPrefab;
        
        [Space]
        [SerializeField] private int maxSize;
        [SerializeField] private int defaultCapacity;
        [SerializeField] private bool collectionCheck;

        private float riseAbove;
        
        private List<BaseFlyRedEnemy> listFlyRedEnemy;
        
        public IObjectPool<BaseFlyRedEnemy> pool;

        private void Awake()
        {
            pool = new ObjectPool<BaseFlyRedEnemy>(
                CreateFlyRedEnemy,
                OnGet,
                OnRelease,
                onDestroyFlyRedEnemy, collectionCheck,
                defaultCapacity,
                maxSize);
            
            listFlyRedEnemy = new List<BaseFlyRedEnemy>();
        }
        
        private BaseFlyRedEnemy CreateFlyRedEnemy()
        {
            var flyRedEnemy = Instantiate(flyRedEnemyPrefab, transform.position, Quaternion.identity);
            flyRedEnemy.Init(target);
            flyRedEnemy.Pool = pool;
            return flyRedEnemy;
        }
        
        private void OnGet(BaseFlyRedEnemy flyRedEnemy)
        {
            flyRedEnemy.transform.position = transform.position;
            flyRedEnemy.transform.rotation = transform.rotation;
            
            flyRedEnemy.MoveToUpY = riseAbove;
            
            flyRedEnemy.gameObject.SetActive(true);
            flyRedEnemy.Pool = pool;
            listFlyRedEnemy.Add(flyRedEnemy);
        }
        
        private void OnRelease(BaseFlyRedEnemy flyRedEnemy)
        {
            flyRedEnemy.gameObject.SetActive(false);
        }
        
        private void onDestroyFlyRedEnemy(BaseFlyRedEnemy flyRedEnemy)
        {
            Destroy(flyRedEnemy.gameObject);
        }

        public void SetDistanceToFly(float distance)
        {
            riseAbove = distance;
        }
        
        public int GetCountActiveFlyRedEnemy()
        {
            int count = 0;

            foreach (var enemy in listFlyRedEnemy)
            { 
                if (enemy.isActiveAndEnabled == false) 
                    continue;
                    
                count++;
            }
            
            return count;
        }

        public void DeactivateFlyRedEnemy()
        {
            foreach (var flyRedEnemy in listFlyRedEnemy)
            {
                if(flyRedEnemy.isActiveAndEnabled == false) 
                    continue;
                
                PlayerBullet.enemyKills++;
                flyRedEnemy.FlyRedEnemy.Deactivate();
            }
        }
        
    }
}