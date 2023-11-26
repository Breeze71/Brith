using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EntityBase _enemyBase, EnemyStateMachine _enemyStateMachine) : base(_enemyBase, _enemyStateMachine)
        {
        
        }
        public override void EnterState() 
        {
            base.EnterState();

            enemyBase.EnemyAttackBaseInstance.DoEnterState();
        }
        
        public override void FrameUpdate() 
        {
            base.FrameUpdate();

            enemyBase.EnemyAttackBaseInstance.DoFrameUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            enemyBase.EnemyAttackBaseInstance.DoPhysicsUpdate();
        }

        public override void ExitState()
        {
            base.ExitState();

            enemyBase.EnemyAttackBaseInstance.DoExitState();
        }

        public override void AnimTriggerEvent(EntityBase.AnimTriggerTypes _triggerTypes)
        {
            base.AnimTriggerEvent(_triggerTypes);

            enemyBase.EnemyAttackBaseInstance.DoAnimTriggerEvent(_triggerTypes);
        }
    }


}
