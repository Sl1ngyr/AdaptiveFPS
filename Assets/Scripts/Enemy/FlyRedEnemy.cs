using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class FlyRedEnemy : Enemy, IDeactivatable
    {
        private const float RIZE_ABOVE_OTHERS = 0.5f;
        
        [SerializeField] private BaseFlyRedEnemy baseBaseFlyRedEnemy;
        
        private bool isLastTweenPlay = false;
        private float time = 1;
        private float repeatTime = 1;
        private float duration = 3.5f;
        
        private Tweener _tweener;

        public bool IsMoveUpComplete { get; set; }
        
        private void Update()
        {

            if (_tweener != null && !isLastTweenPlay)
            {
                if (!_tweener.IsActive())
                {
                    time -= Time.deltaTime;
                    if (time <= 0)
                    {
                        time = repeatTime;
                        transform.DOMoveY(baseBaseFlyRedEnemy.gameObject.transform.position.y, duration).SetEase(Ease.InQuad);
                        isLastTweenPlay = true;
                        IsMoveUpComplete = true;
                    }
                }
            }
            else if(_tweener == null)
            {
                _tweener = transform.DOMoveY(baseBaseFlyRedEnemy.MoveToUpY + RIZE_ABOVE_OTHERS, duration).SetEase(Ease.Linear);
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ResetValue();
                baseBaseFlyRedEnemy.Pool.Release(baseBaseFlyRedEnemy);
            }
        }
        
        private void ResetValue()
        {
            isLastTweenPlay = false;
            _tweener = null;
            IsMoveUpComplete = false;
            transform.position = baseBaseFlyRedEnemy.transform.position;
        }

        public void Deactivate()
        {
            ResetValue();
            baseBaseFlyRedEnemy.Pool.Release(baseBaseFlyRedEnemy);
        }
    }
}
