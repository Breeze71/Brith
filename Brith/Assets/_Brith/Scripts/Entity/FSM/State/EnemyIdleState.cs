using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    /// <summary>
    /// Enemy Idle State
    /// </summary>
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(EntityBase _enemyBase, EnemyStateMachine _enemyStateMachine) : base(_enemyBase, _enemyStateMachine)
        {
        }

        public override void EnterState() 
        {
            base.EnterState();

            enemyBase.EnemyIdleBaseInstance.DoEnterState();
        }
        
        public override void FrameUpdate() 
        {
            base.FrameUpdate();

            enemyBase.EnemyIdleBaseInstance.DoFrameUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            enemyBase.EnemyIdleBaseInstance.DoPhysicsUpdate();
        }

        public override void ExitState()
        {
            base.ExitState();

            enemyBase.EnemyIdleBaseInstance.DoExitState();
        }

        public override void AnimTriggerEvent(EntityBase.AnimTriggerTypes _triggerTypes)
        {
            base.AnimTriggerEvent(_triggerTypes);

            enemyBase.EnemyIdleBaseInstance.DoAnimTriggerEvent(_triggerTypes);
        }
    }
}
