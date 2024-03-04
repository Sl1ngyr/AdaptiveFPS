using Bullet;
using Enemy;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerUltimate : MonoBehaviour
    {
        private const int COUNT_POWER_FOR_KILL_BLUE_ENEMY = 50;
        private const int COUNT_POWER_FOR_KILL_FLY_RED_ENEMY = 15;
        private const int MAX_POWER = 100;
        private const string BLUE_ENEMY = "BlueEnemy";
        private const string FLY_RED_ENEMY = "FlyRedEnemy";
        
        [SerializeField] private int power = 50;
        [SerializeField] private EnemySpawnManager enemySpawnManager;
        [SerializeField] private UltimateUI ultimateUI;
        
        private PlayerInputActions UltimateInputAction;
        private int blueEnemyBulletHit = 25;
        
        private void Awake()
        {
            UltimateInputAction = new PlayerInputActions();
            UltimateInputAction.Player.Enable();
            UltimateInputAction.Player.Ultimate.performed += Ultimate;
            
            ultimateUI.SetStats(power);
        }

        private void Ultimate(InputAction.CallbackContext obj)
        {
            if (power == MAX_POWER)
            {
                enemySpawnManager.DeactivateAllEnemy();
                power = 0;
                ultimateUI.SetStats(power);
            }
        }

        private void OnEnable()
        {
            PlayerBullet.onKilled += IncreasePower;
        }

        private void OnDisable()
        {
            UltimateInputAction.Player.Ultimate.performed -= Ultimate;
            PlayerBullet.onKilled -= IncreasePower;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.CompareTag("EnemyBullet"))
            {
                if(power != 0)
                    power -= blueEnemyBulletHit;
                
                ultimateUI.SetStats(power);
            }
        }

        private void IncreasePower(string enemy)
        {
            if (power == MAX_POWER) 
                return;
            
            int countOfPowerForKillEnemy = 0;
            int difference;
            
            if (enemy == BLUE_ENEMY)
                countOfPowerForKillEnemy = COUNT_POWER_FOR_KILL_BLUE_ENEMY;
            else if (enemy == FLY_RED_ENEMY)
                countOfPowerForKillEnemy = COUNT_POWER_FOR_KILL_FLY_RED_ENEMY;

            difference = MAX_POWER - power;
            
            if (difference >= countOfPowerForKillEnemy)
            {
                power += countOfPowerForKillEnemy;
                ultimateUI.SetStats(power);
            }
            else if (difference < countOfPowerForKillEnemy)
            {
                power += difference;
                ultimateUI.SetStats(power);
            }
        }
    }
}