using Bullet;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RestartGame : MonoBehaviour
    {
        private Button restartSceneButton;

        private void Start()
        {
            restartSceneButton = GetComponent<Button>();
            restartSceneButton.onClick.AddListener(RestartGameScene);
        }
        
        private void OnDestroy()
        {
            restartSceneButton.onClick.RemoveListener(RestartGameScene);
        }
        
        private void RestartGameScene()
        {
            Time.timeScale = 1;
            DOTween.KillAll();
            PlayerBullet.enemyKills = 0;
            SceneManager.LoadScene("GameScene");
        }
    }
}