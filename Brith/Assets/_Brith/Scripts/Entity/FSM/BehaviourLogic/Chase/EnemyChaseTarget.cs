using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    [CreateAssetMenu(fileName = "Chase", menuName = "EnemySO/Chase/ChaseTarget", order = 0)]
    public class EnemyChaseTarget : EnemyChaseSOBase
    {
        [SerializeField] private float chasingSpeed = 5f;

        public override void DoEnterState()
        {
            base.DoEnterState();

            // enemyBase.Rb.velocity = Vector2.zero;
        }

        public override void DoFrameUpdate()
        {
            base.DoFrameUpdate();
            Debug.Log("Chase");

            if(enemyBase.TargetTransform != null)
            {
                Vector2 _moveDirection = (enemyBase.TargetTransform.position - enemyBase.transform.position).normalized;
                
                // enemyBase.SetVelocity(_moveDirection * chasingSpeed);
                enemyBase.Rb.AddForce(_moveDirection * chasingSpeed, ForceMode2D.Impulse);

                enemyBase.StateMachine.ChangeState(enemyBase.IdleState);
            }
        }
    }
}
