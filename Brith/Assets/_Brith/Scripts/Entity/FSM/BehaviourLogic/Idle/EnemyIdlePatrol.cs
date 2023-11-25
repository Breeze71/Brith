using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    /// <summary>
    /// Random Move
    /// </summary>
    [CreateAssetMenu(fileName = "IdlePatrol", menuName = "EnemySO/Idle/Patrol", order = 0)]
    public class EnemyIdlePatrol : EnemyIdleSOBase
    {
        [SerializeField] private float patrolMoveRange = 5f;
        [SerializeField] private float patrolSpeed = 1f;

        private Vector3 targetPos;
        private Vector3 direction;

        public override void DoEnterState()
        {
            base.DoEnterState();

            targetPos = GetRandomPointCircle();
        }

        public override void DoFrameUpdate()
        {
            base.DoFrameUpdate();

            direction = (targetPos - enemyBase.transform.position).normalized;

            enemyBase.SetVelocity(direction * patrolSpeed);

            if((enemyBase.transform.position - targetPos).sqrMagnitude <= .5) // 向量轉長度
            {
                targetPos = GetRandomPointCircle();
            }
        }

        private Vector3 GetRandomPointCircle()
        {
            return enemyBase.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * patrolMoveRange;
        }
    }
}
