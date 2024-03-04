using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Bullet
{
    public class BlueEnemyBullet : Bullet
    {
        private NavMeshAgent agent;
        private Transform target;
        private Transform spawnPosition;

        [SerializeField] public Transform flyBullet;
        
        public IObjectPool<BlueEnemyBullet> Pool { get; set; }
        
        public void Init(Transform _target)
        {
            target = _target;
        }
        
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            FollowTargetPosition();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Pool?.Release(this);
            }
        }

        private void FollowTargetPosition()
        {
            var targetPosition = target.position;
            agent.destination = targetPosition;
        }
        
    }
}