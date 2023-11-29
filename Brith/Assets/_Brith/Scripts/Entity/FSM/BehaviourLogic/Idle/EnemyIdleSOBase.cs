using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class EnemyIdleSOBase : ScriptableObject 
    {
        protected EntityBase enemyBase;
        protected Transform transform;  // SO 沒有 trasnform
        protected GameObject gameObject;

        protected Transform playerTransform;

        public virtual void Initialize(GameObject _gameObject, EntityBase _enemyBase)
        {
            gameObject = _gameObject;
            transform = gameObject.transform;
            enemyBase = _enemyBase;

            playerTransform = enemyBase.TargetTransform; 
        }

        public virtual void DoEnterState() {}
        public virtual void DoExitState() 
        { 
            ResetValue(); 
        }
        public virtual void DoFrameUpdate() 
        {
            if(enemyBase.IsAggroed)
            {
                enemyBase.StateMachine.ChangeState(enemyBase.ChaseState);
            }
        }
        public virtual void DoPhysicsUpdate() {}
        public virtual void DoAnimTriggerEvent(EntityBase.AnimTriggerTypes _triggerTypes) {}
        public virtual void ResetValue() {}
    }
}
