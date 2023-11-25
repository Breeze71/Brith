using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    [CreateAssetMenu(fileName = "Chase", menuName = "EnemySO/Chase/ChasePlayer", order = 0)]
    public class EnemyChasePlayer : EnemyChaseSOBase
    {
        [SerializeField] private float chasingSpeed = 5f;

        public override void DoFrameUpdate()
        {
            base.DoFrameUpdate();

            Vector2 _moveDirection = (playerTransform.position - enemyBase.transform.position).normalized;

            enemyBase.SetVelocity(_moveDirection * chasingSpeed);
        }
    }
}
