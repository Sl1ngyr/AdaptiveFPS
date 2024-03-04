using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [HideInInspector] 
        protected Transform target;

        public float GetBoundsY()
        {
            float bounds = transform.localScale.y + transform.position.y;
            return bounds;
        }
    }
}