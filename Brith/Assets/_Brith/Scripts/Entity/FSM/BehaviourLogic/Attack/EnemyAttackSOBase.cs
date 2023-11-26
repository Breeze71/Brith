using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class EnemyAttackSOBase : ScriptableObject
    {
        protected EntityBase enemyBase;
        protected Transform transform;  // SO 沒有 trasnform
        protected GameObject gameObject;

        protected Transform targetTransform;

        public virtual void Initialize(GameObject _gameObject, EntityBase _enemyBase)
        {
            gameObject = _gameObject;
            transform = gameObject.transform;
            enemyBase = _enemyBase;
        }

        public virtual void DoEnterState() 
        {
            targetTransform = enemyBase.TargetTransform; 
        }
        public virtual void DoExitState() { ResetValue(); }
        public virtual void DoFrameUpdate() 
        {
            if(!enemyBase.IsInAttackRange)
            {
                enemyBase.StateMachine.ChangeState(enemyBase.IdleState);
            }
        }
        public virtual void DoPhysicsUpdate() {}
        public virtual void DoAnimTriggerEvent(EntityBase.AnimTriggerTypes _triggerTypes) {}
        public virtual void ResetValue() {}
    }
}
