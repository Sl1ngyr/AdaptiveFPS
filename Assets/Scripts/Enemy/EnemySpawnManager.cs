using UnityEngine;

namespace Enemy
{
    public class EnemySpawnManager : MonoBehaviour
    {
        private const int MAX_ENEMY_ON_MAP = 30;
        private const int TOTAL_QUANTITY_ENEMY_TO_SPAWN = 5;
        
        [SerializeField] private BlueEnemyObjectPool blueEnemy;
        [SerializeField] private FlyRedEnemyObjectPool flyRedEnemy;

        [SerializeField] private float intervalToSpawnEnemy;
        [SerializeField] private int countIntervalToSpawnEnemy;
        
        private int reducingIntervalSpawn = 2;
        private int countFlyRedEnemyToSpawn = 4;
        private int countOfEnemyOnMap;
        
        private void Update()
        {
            intervalToSpawnEnemy -= Time.deltaTime;
            if (intervalToSpawnEnemy <= 0)
            {
                SpawnEnemy();

                if (countIntervalToSpawnEnemy == 6)
                    intervalToSpawnEnemy = countIntervalToSpawnEnemy;
                else
                {
                    countIntervalToSpawnEnemy -= reducingIntervalSpawn;
                    intervalToSpawnEnemy = countIntervalToSpawnEnemy;
                }
                
            }
            
        }


        private void SpawnEnemy()
        {
            int difference = MAX_ENEMY_ON_MAP - CalculateCountOfEnemyOnMap();

            if (difference == 0) return;
            
            if (TOTAL_QUANTITY_ENEMY_TO_SPAWN > difference)
            {
                blueEnemy.pool.Get();
                difference -= 1;
                    
                for (int i = 0; i < difference; i++)
                {
                    flyRedEnemy.SetDistanceToFly(blueEnemy.GetBlueEnemyBoundsY());
                    flyRedEnemy.pool.Get();
                }
                    
            }
            else if (difference >= TOTAL_QUANTITY_ENEMY_TO_SPAWN)
            {
                blueEnemy.pool.Get();
                
                for (int i = 0; i < countFlyRedEnemyToSpawn; i++)
                {
                    flyRedEnemy.SetDistanceToFly(blueEnemy.GetBlueEnemyBoundsY());
                    flyRedEnemy.pool.Get();
                }
            }
        }
        
        private int CalculateCountOfEnemyOnMap()
        {
            int count = 0;
            count += blueEnemy.GetCountActiveBlueEnemy();
            count += flyRedEnemy.GetCountActiveFlyRedEnemy();
            return count;
        }

        public void DeactivateAllEnemy()
        {
            blueEnemy.DeactivateBlueEnemy();
            flyRedEnemy.DeactivateFlyRedEnemy();
        }
    }
}