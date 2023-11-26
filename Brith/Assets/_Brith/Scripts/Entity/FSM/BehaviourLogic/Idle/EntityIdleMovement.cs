using UnityEngine;

namespace V
{
    /// <summary>
    /// 以固定的速度运动，只有两种情况会改变单位的运动情况，即撞到环境物体或者撞到别的单位(用物理系統解決)
    /// </summary>
    [CreateAssetMenu(fileName = "EntityIdle", menuName = "EnemySO/Idle/EntityIdle", order = 1)]
    public class EntityIdleMovement : EnemyIdleSOBase
    {
        private Vector2 moveDiretion;

        public override void DoEnterState()
        {
            base.DoEnterState();

            // To - Do 改為投放方向
            Vector2 _randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            enemyBase.Rb.AddForce(_randomDirection * enemyBase.speed, ForceMode2D.Impulse);

            moveDiretion = enemyBase.Rb.velocity.normalized;
        }
        

        public override void DoFrameUpdate()
        {
            base.DoFrameUpdate();

            // if current velocity less then minStable Speed
            if(enemyBase.Rb.velocity.magnitude < enemyBase.speed * 0.9)
            {
                enemyBase.Rb.AddForce(moveDiretion * enemyBase.speed, ForceMode2D.Impulse);
            }

            // if current velocity more than minStable speed
            else if(enemyBase.Rb.velocity.magnitude > enemyBase.speed * 1.1)
            {
                Vector2 _currentDir = enemyBase.Rb.velocity.normalized;

                enemyBase.Rb.velocity = new Vector2(_currentDir.x, _currentDir.y) * enemyBase.speed;
            }
        }

        public override void DoExitState()
        {
            base.DoExitState();
        }
    }
}
