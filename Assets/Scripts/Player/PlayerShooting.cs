using Bullet;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private PlayerBulletSpawner playerBulletSpawner;

        private PlayerInputActions playerInputActions;
        
        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Shoot.performed += Shoot;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            playerBulletSpawner.pool?.Get();
        }

        private void OnDisable()
        {
            playerInputActions.Player.Shoot.performed -= Shoot;
        }
        
    }
}