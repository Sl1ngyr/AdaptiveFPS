using System;
using Enemy;
using UnityEngine;

namespace Bullet
{
    public class PlayerBullet : Bullet
    {
        private float bulletSpeed = 50;
        private float maxDistance = 1;

        public static Action<string> onKilled;
        public static int enemyKills;
        
        private void FixedUpdate()
        {
            RaycastHit hit;
            
            if (Physics.Raycast(bulletRigidbody.transform.position, bulletRigidbody.transform.forward, out hit,maxDistance))
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    var enemy = hit.collider.GetComponent<IDeactivatable>();
                    
                    if(enemy.GetType() == typeof(BlueEnemy)) 
                        onKilled?.Invoke("BlueEnemy");
                    else if(enemy.GetType() == typeof(FlyRedEnemy))
                        onKilled?.Invoke("FlyRedEnemy");
                    
                    enemy.Deactivate();
                    enemyKills++;
                    Pool?.Release(this);
                }
                if(hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("Obstacle")) 
                    Pool?.Release(this);
            }
            
            if (Physics.Raycast(bulletRigidbody.transform.position, bulletRigidbody.transform.forward * -1, out hit, maxDistance))
            {
                if(hit.transform.gameObject.CompareTag("Wall")) 
                    Pool?.Release(this);
            }
            
            bulletRigidbody.velocity = transform.forward * bulletSpeed;
        }
        
    }
}