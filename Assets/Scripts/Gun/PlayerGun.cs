using UnityEngine;

namespace Gun
{
    public class PlayerGun : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        private void Start()
        {
            PlayerGunSetPosition();
        }

        private void Update()
        {
            PlayerGunSetRotation();
        }

        private void PlayerGunSetRotation()
        {
            transform.localRotation = mainCamera.transform.localRotation;
        }
        
        private void PlayerGunSetPosition()
        {
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        }
    }
}