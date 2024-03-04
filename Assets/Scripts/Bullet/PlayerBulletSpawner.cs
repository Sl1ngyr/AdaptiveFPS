using UnityEngine;
using UnityEngine.Pool;

namespace Bullet
{
    public class PlayerBulletSpawner : MonoBehaviour
    {
        [SerializeField] private Transform gun;
        [SerializeField] private Bullet bulletPrefab;
        
        [Space]
        [SerializeField] private int maxSize;
        [SerializeField] private int defaultCapacity;
        [SerializeField] private bool collectionCheck;
        
        public IObjectPool<Bullet> pool;

        private void Awake()
        {
            pool = new ObjectPool<Bullet>(
                CreateBullet,
                OnGet,
                OnRelease,
                OnDestroyBullet, collectionCheck,
                defaultCapacity,
                maxSize);
        }
        
        private Bullet CreateBullet()
        {
            var bulletElement = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
            bulletElement.Pool = pool;
            return bulletElement;
        }
        
        private void OnGet(Bullet bullet)
        {
            bullet.transform.position = gun.position;
            bullet.transform.rotation = gun.rotation;
            
            bullet.gameObject.SetActive(true);
            bullet.Pool = pool;
        }
        
        private void OnRelease(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
        
        private void OnDestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }
        
    }
    
}