using Bullet;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void GameOverUI()
        {
            transform.gameObject.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
            
            text.text = "Enemies Killed: " + PlayerBullet.enemyKills;
            
            Time.timeScale = 0;
        }
    }
}