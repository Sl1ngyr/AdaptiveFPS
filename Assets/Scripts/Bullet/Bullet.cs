using UnityEngine;
using UnityEngine.Pool;

namespace Bullet
{
    public abstract class Bullet : MonoBehaviour
    {
        protected Rigidbody bulletRigidbody;

        private void Start()
        {
            bulletRigidbody = GetComponent<Rigidbody>();
        }

        public IObjectPool<Bullet> Pool { get; set; }
    }
}