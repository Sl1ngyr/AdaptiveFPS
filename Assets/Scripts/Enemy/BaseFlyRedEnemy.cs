using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Enemy
{
    public class BaseFlyRedEnemy : Enemy
    {
        [SerializeField] private FlyRedEnemy flyRedEnemy;

        private NavMeshAgent agent;
        
        public FlyRedEnemy FlyRedEnemy => flyRedEnemy;
        
        public float MoveToUpY { get; set; }

        public IObjectPool<BaseFlyRedEnemy> Pool { get; set; }
        
        
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
            
            if (flyRedEnemy.IsMoveUpComplete)
            {
                agent.destination = target.position;
            }
            
        }
        
    }
    
}