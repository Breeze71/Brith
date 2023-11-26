

namespace V
{
    public class EnemyState
    {
        protected EntityBase enemyBase;
        protected EnemyStateMachine enemyStateMachine;

        public EnemyState(EntityBase _enemyBase, EnemyStateMachine _enemyStateMachine)
        {
            enemyBase = _enemyBase;
            enemyStateMachine = _enemyStateMachine;
        }

        public virtual void EnterState() {}
        public virtual void ExitState() {}
        public virtual void FrameUpdate() {}
        public virtual void PhysicsUpdate() {}
        public virtual void AnimTriggerEvent(EntityBase.AnimTriggerTypes _triggerTypes) {}
    }
}
