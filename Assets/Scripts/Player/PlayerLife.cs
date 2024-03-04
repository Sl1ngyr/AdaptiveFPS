using UI;
using UnityEngine;

namespace Player
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private HealthBarUI healthBarUI;
        [SerializeField] private GameOver gameOver;
        
        private int life = 100;
        private int enemyLayer = 11;
        private int enemyHit = 15;

        private void Awake()
        {
            healthBarUI.SetStats(life);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.gameObject.layer == enemyLayer)
            {
                if (life > enemyHit)
                {
                    life -= enemyHit;
                    healthBarUI.SetStats(life);
                }
                else if (life < enemyHit)
                {
                    life = 0;
                    healthBarUI.SetStats(life);
                    
                    gameOver.GameOverUI();
                }
            }
        }
        
    }
}