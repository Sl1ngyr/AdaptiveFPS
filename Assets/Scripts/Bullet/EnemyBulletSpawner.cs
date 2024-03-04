using UnityEngine;
using UnityEngine.Pool;

namespace Bullet
{
    public class EnemyBulletSpawner : MonoBehaviour
    {
        [SerializeField] private BlueEnemyBullet bulletPrefab;
        [SerializeField] private Transform target;
        
        [Space]
        [SerializeField] private int maxSize;
        [SerializeField] private int defaultCapacity;
        [SerializeField] private bool collectionCheck;
        
        private Vector3 gun;
        public IObjectPool<BlueEnemyBullet> pool;

        private void Awake()
        {
            pool = new ObjectPool<BlueEnemyBullet>(
                CreateBullet,
                OnGet,
                OnRelease,
                OnDestroyBullet, collectionCheck,
                defaultCapacity,
                maxSize);
        }
        
        private BlueEnemyBullet CreateBullet()
        {
            var bulletElement = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletElement.Init(target);
            bulletElement.Pool = pool;
            return bulletElement;
        }
        
        private void OnGet(BlueEnemyBullet bullet)
        {

            bullet.transform.position = new Vector3(gun.x, 0, gun.z);
            bullet.flyBullet.position = new Vector3(bullet.transform.position.x, gun.y, bullet.transform.position.z);
            bullet.gameObject.SetActive(true);
            bullet.Pool = pool;
        }
        
        private void OnRelease(BlueEnemyBullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
        
        private void OnDestroyBullet(BlueEnemyBullet bullet)
        {
            Destroy(bullet.gameObject);
        } 

        public void SetSpawnPosition(Vector3 pos)
        {
            gun = pos;
        }
    }
    
}