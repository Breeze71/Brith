using UnityEngine;

namespace V
{
    /// <summary>
    /// 以固定的速度运动，只有两种情况会改变单位的运动情况，即撞到环境物体或者撞到别的单位(用物理系統解決)
    /// </summary>
    [CreateAssetMenu(fileName = "EntityIdle", menuName = "EnemySO/Idle/EntityIdle", order = 1)]
    public class EntityIdleMovement : EnemyIdleSOBase
    {
        [SerializeField] private float minStableSpeed;

        private Vector2 moveDiretion;

        public override void DoEnterState()
        {
            base.DoEnterState();

            Vector2 _randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            enemyBase.Rb.AddForce(_randomDirection * minStableSpeed, ForceMode2D.Impulse);

            moveDiretion = _randomDirection;
        }

        public override void DoFrameUpdate()
        {
            base.DoFrameUpdate();

            // if current velocity less then minStable Speed
            if(enemyBase.Rb.velocity.magnitude < minStableSpeed * 0.7)
            {
                enemyBase.Rb.AddForce(moveDiretion * minStableSpeed, ForceMode2D.Impulse);
            }

            // if current velocity more than minStable speed
            else if(enemyBase.Rb.velocity.magnitude > minStableSpeed * 1.2)
            {
                Vector2 _currentDir = enemyBase.Rb.velocity.normalized;

                enemyBase.Rb.velocity = new Vector2(_currentDir.x, _currentDir.y) * minStableSpeed;
            }
        }

        public override void DoExitState()
        {
            base.DoExitState();
        }
    }
}
