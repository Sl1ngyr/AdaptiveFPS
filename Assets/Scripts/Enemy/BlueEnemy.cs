using UnityEngine;
using Bullet;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Enemy
{
    public class BlueEnemy : Enemy, IDeactivatable
    {
        private const float INTERVAL = 3;
        
        private EnemyBulletSpawner bullet;
        private NavMeshAgent agent;
        private Collider capsuleCollider;
        
        [SerializeField] private float stoppingDistance;
        [SerializeField] private Transform bulletPos;
        
        private float shootingInterval = 3;
        
        public IObjectPool<BlueEnemy> Pool { get; set; }
        
        public void Init(Transform _target, EnemyBulletSpawner _bullet)
        {
            target = _target;
            bullet = _bullet;
        }
        
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            capsuleCollider = GetComponent<Collider>();
        }
    
        private void Update()
        {
            if (CalculateDistanceToTarget() < stoppingDistance)
            {
                StopFollow();
            }
            else
            {
                FollowTargetPosition();
            }

            Shoot();
        }

        private void FollowTargetPosition()
        {
            agent.isStopped = false;
            agent.destination = target.position;
        }
        
        private void StopFollow()
        {
            agent.isStopped = true;
        }

        private float CalculateDistanceToTarget()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            
            return distance;
        }

        private void Shoot()
        {
            shootingInterval -= Time.deltaTime;
            
            if (shootingInterval <= 0)
            {
                bullet.SetSpawnPosition(new Vector3(bulletPos.position.x, capsuleCollider.bounds.center.y, bulletPos.position.z));
                bullet.pool.Get();
                shootingInterval = INTERVAL;
            }
        }

        public void Deactivate()
        {
            Pool?.Release(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Pool?.Release(this);
            }
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("PlayerBullet"))
            {
                Pool?.Release(this);
            }
        }
    }
}